using UnityEngine;

public class InputHandler : MonoBehaviour {

    public GameObject Player;
    public enum SwipeDirection { UPSWIPE, DOWNSWIPE, LEFTSWIPE, RIGHTSWIPE, NOSWIPE}

    void Update () {
        CheckBackButton();
        CheckInput();
    }

    private void CheckBackButton()
    {
        if (Input.GetKeyDown("escape"))
        {
            //Android Back button was pressed.
            //load the previous UI screen
            MenuManager.Instance.PopMenu();
        }
    }

#if UNITY_IPHONE || UNITY_ANDROID
    private void CheckInput()
    {

        if (Input.touchCount > 0)
        {
            Debug.Log("[InputHandler] TouchCount = " + Input.touchCount);
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    mTouchStart = touch.position;
                    mStartTime = Time.time;
                    //mSwipeDirection = SwipeDirection.NOSWIPE;
                    mIsPossibleSwipe = true;
                    Debug.Log("[InputHandler] began swipe = true");
                    break;
                case TouchPhase.Moved:
                    //Check for swipe out of bounds
                    if (mIsPossibleSwipe)
                    {
                        if (Mathf.Abs(touch.position.y - mTouchStart.y) > mEpsilon &&
                            Mathf.Abs(touch.position.x - mTouchStart.x) > mEpsilon)
                        {
                            Debug.Log("[InputHandler] Moved swipe = false");
                            mIsPossibleSwipe = false;
                        }
                    }
                    break;
                //case TouchPhase.Canceled:
                //case TouchPhase.Stationary:
                //    Debug.Log("[InputHandler] Moved cancelled or stationary = false");
                //    mIsPossibleSwipe = false;
                //    break;
                case TouchPhase.Ended:
                    float elapsedTime = Time.time - mStartTime;
                    if (mIsPossibleSwipe && elapsedTime < mMaxSwipeTime)
                    {
                        Debug.Log("[InputHandler] Ended Possible Swipe");
                        Vector2 touchDistance;
                        touchDistance = new Vector2(Mathf.Abs(touch.position.x - mTouchStart.x), Mathf.Abs(touch.position.y - mTouchStart.y));

                        if (touchDistance.x > touchDistance.y && touchDistance.x > mMinSwipeDistance)
                        {
                            Debug.Log("[InputHandler] Ended Possible Horizontal Swipe");
                            //Horizontal swipe attempt
                            if (touch.position.x - mTouchStart.x < 0)
                            {
                                PlayerMovement.Instance.EnableDefaultShape();
                            }
                            else
                            {
                                PlayerMovement.Instance.Burst();
                            }
                        }
                        else if (touchDistance.y >= touchDistance.x && touchDistance.y > mMinSwipeDistance)
                        {
                            Debug.Log("[InputHandler] Ended Possible Vertical Swipe");
                            //Vertical swipe attempt
                            if (touch.position.y - mTouchStart.y < 0)
                            {
                                PlayerMovement.Instance.Drop();
                            }
                            else
                            {
                                PlayerMovement.Instance.Jump();
                            }
                        }
                        else
                        {
                            //No Swipe
                            Debug.Log("[InputHandler] Ended no Swipe");
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
#else
    private void CheckInput()
    {
        if (Input.GetKeyDown("w"))
        {
            PlayerMovement.Instance.Jump();
            Debug.Log("[TouchHandler] w ");
        }
        else if (Input.GetKeyDown("a"))
        {
            PlayerMovement.Instance.EnableDefaultShape();
            Debug.Log("[TouchHandler] a ");
        }
        else if (Input.GetKeyDown("d"))
        {
            PlayerMovement.Instance.Burst();
            Debug.Log("[TouchHandler] d ");
        }
        else if (Input.GetKeyDown("s"))
        {
            PlayerMovement.Instance.Drop();
            Debug.Log("[TouchHandler] s ");
        }
    }
#endif

    private Vector2 mTouchStart;
    private float mStartTime;
    private bool mIsPossibleSwipe = false;
    private SwipeDirection mSwipeDirection;               //vertical = 0;
    private float mEpsilon = 200.0f;
    private float mMinSwipeDistance = 50.0f;
    private float mMaxSwipeTime = 1.0f;
}
