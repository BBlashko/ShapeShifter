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
        
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    mTouchStart = touch.position;
                    mStartTime = Time.time;
                    mSwipeDirection = SwipeDirection.NOSWIPE;
                    mDirectionChosen = false;
                    //Debug.Log("[TouchHandler] Touch began");
                    break;
                case TouchPhase.Moved:
                    //Debug.Log("[TouchHandler] Touch moved");
                    if (!mDirectionChosen)
                    {
                        Debug.Log("[TouchHandler] Touch moved");
                        if (Mathf.Abs(touch.position.y - mTouchStart.y) < mEpsilon &&
                            Mathf.Abs(touch.position.x - mTouchStart.x) > mMinSwipeDistance)
                        {
                            if (touch.position.x < mTouchStart.x)
                            {
                                mDirectionChosen = true;
                                mSwipeDirection = SwipeDirection.LEFTSWIPE;
                                Debug.Log("[TouchHandler] Touch moved");
                            }
                            else
                            {
                                mDirectionChosen = true;
                                mSwipeDirection = SwipeDirection.RIGHTSWIPE;
                            }
                        }
                        else if (Mathf.Abs(touch.position.y - mTouchStart.y) > mMinSwipeDistance &&
                                 Mathf.Abs(touch.position.x - mTouchStart.x) < mEpsilon)
                        {
                            if (touch.position.y < mTouchStart.y)
                            {
                                mDirectionChosen = true;
                                mSwipeDirection = SwipeDirection.DOWNSWIPE;
                            }
                            else
                            {
                                mDirectionChosen = true;
                                mSwipeDirection = SwipeDirection.UPSWIPE;
                            }
                        }
                    }
                    if (Mathf.Abs(touch.position.y - mTouchStart.y) > mEpsilon &&
                        Mathf.Abs(touch.position.x - mTouchStart.x) > mEpsilon &&
                        mSwipeDirection != SwipeDirection.NOSWIPE)
                    {
                        mSwipeDirection = SwipeDirection.NOSWIPE;
                        Debug.Log("[TouchHandler] touch cancelled! outside of epsilon range.");
                    }
                    break;
                case TouchPhase.Canceled:
                    Debug.Log("[TouchHandler] touch canceled");
                    break;
                case TouchPhase.Stationary:

                    //mSwipeDirection = SwipeDirection.NOSWIPE;
                    Debug.Log("[TouchHandler] touch stationary");
                    break;
                case TouchPhase.Ended:
                    Debug.Log("[TouchHandler] touch ended");
                    float elapsedTime = Time.time - mStartTime;
                    //Debug.Log("[TouchHandler] elapsed swipe time = " + elapsedTime);
                    if (elapsedTime < mMaxSwipeTime)
                    {
                        float touchDistance;
                        switch (mSwipeDirection)
                        {
                            case SwipeDirection.NOSWIPE:
                                Debug.Log("[TouchHandler] NO SWIPE");
                                break;
                            case SwipeDirection.UPSWIPE:
                                touchDistance = touch.position.y - mTouchStart.y;
                                if (touchDistance > mMinSwipeDistance)
                                {
                                    PlayerMovement.Instance.Jump();
                                    Debug.Log("[TouchHandler] UPSWIPE");
                                    Debug.Log("[TouchHandler] y distance = " + touchDistance);
                                }
                                else
                                {
                                    Debug.Log("[TouchHandler] no swipe x distance = " + touchDistance);
                                }
                                break;
                            case SwipeDirection.DOWNSWIPE:
                                PlayerMovement.Instance.Drop();
                                touchDistance = mTouchStart.y - touch.position.y;
                                if (touchDistance > mMinSwipeDistance)
                                {
                                    //DO SOMETHING
                                    Debug.Log("[TouchHandler] DOWNSWIPE");
                                    Debug.Log("[TouchHandler] y distance = " + touchDistance);
                                }
                                else
                                {
                                    Debug.Log("[TouchHandler] no swipe x distance = " + touchDistance);
                                }
                                break;
                            case SwipeDirection.LEFTSWIPE:
                                touchDistance = mTouchStart.x - touch.position.x;
                                if (touchDistance > mMinSwipeDistance)
                                {
                                    PlayerMovement.Instance.EnableDefaultShape();
                                    Debug.Log("[TouchHandler] LEFTSWIPE");
                                    Debug.Log("[TouchHandler] x distance = " + touchDistance);
                                }
                                else
                                {
                                    Debug.Log("[TouchHandler] no swipe x distance = " + touchDistance);
                                }
                                break;
                            case SwipeDirection.RIGHTSWIPE:
                                PlayerMovement.Instance.Burst();
                                touchDistance = touch.position.x - mTouchStart.x;
                                if (touchDistance > mMinSwipeDistance)
                                {
                                    Debug.Log("[TouchHandler] RIGHTSWIPE");
                                    Debug.Log("[TouchHandler] x distance = " + touchDistance);
                                }
                                else
                                {
                                    Debug.Log("[TouchHandler] no swipe x distance = " + touchDistance);
                                }
                                break;
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
    private bool mDirectionChosen = false;
    private SwipeDirection mSwipeDirection;               //vertical = 0;
    private float mEpsilon = 200.0f;
    private float mMinSwipeDistance = 50.0f;
    private float mMaxSwipeTime = 1.0f;
}
