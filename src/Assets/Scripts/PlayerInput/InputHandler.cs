﻿using UnityEngine;

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
                    Debug.Log("[TouchHandler] Touch started");
                    break;
                case TouchPhase.Moved:
                    if (!mDirectionChosen)
                    {
                        if (Mathf.Abs(touch.position.y - mTouchStart.y) < mEpsilon &&
                            Mathf.Abs(touch.position.x - mTouchStart.x) > mEpsilon)
                        {
                            if (touch.position.x < mTouchStart.x)
                            {
                                mDirectionChosen = true;
                                mSwipeDirection = SwipeDirection.LEFTSWIPE;
                            }
                            else
                            {
                                mDirectionChosen = true;
                                mSwipeDirection = SwipeDirection.RIGHTSWIPE;
                            }
                        }
                        else if (Mathf.Abs(touch.position.y - mTouchStart.y) > mEpsilon &&
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
                        Debug.Log("[TouchHandler] touch cancelled outside of epsilon range.");
                    }
                    break;
                case TouchPhase.Canceled:
                case TouchPhase.Stationary:
                    mSwipeDirection = SwipeDirection.NOSWIPE;
                    Debug.Log("[TouchHandler] touch cancelled");
                    break;
                case TouchPhase.Ended:
                    float elapsedTime = Time.time - mStartTime;
                    Debug.Log("[TouchHandler] elapsed swipe time = " + elapsedTime);
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
                                    //DO SOMETHING
                                    Debug.Log("[TouchHandler] UPSWIPE");
                                    Debug.Log("[TouchHandler] y distance = " + touchDistance);
                                }
                                else
                                {
                                    Debug.Log("[TouchHandler] no swipe x distance = " + touchDistance);
                                }
                                break;
                            case SwipeDirection.DOWNSWIPE:
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
                                    Debug.Log("[TouchHandler] LEFTSWIPE");
                                    Debug.Log("[TouchHandler] x distance = " + touchDistance);
                                }
                                else
                                {
                                    Debug.Log("[TouchHandler] no swipe x distance = " + touchDistance);
                                }
                                break;
                            case SwipeDirection.RIGHTSWIPE:
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
            }
        }
    }
#else
    private void CheckInput()
    {
        if (Input.GetKeyDown("w"))
        {
            Debug.Log("[TouchHandler] w ");
        }
        else if (Input.GetKeyDown("a"))
        {
            Debug.Log("[TouchHandler] a ");
        }
        else if (Input.GetKeyDown("d"))
        {
            Debug.Log("[TouchHandler] d ");
        }
        else if (Input.GetKeyDown("s"))
        {
            Debug.Log("[TouchHandler] s ");
        }
        else if (Input.GetKeyDown("escape"))
        {
            MenuManager.Instance.PopMenu();
        }
    }
#endif

    private Vector2 mTouchStart;
    private float mStartTime;
    private bool mDirectionChosen = false;
    private SwipeDirection mSwipeDirection;               //vertical = 0;
    private float mEpsilon = 200.0f;
    private float mMinSwipeDistance = 250.0f;
    private float mMaxSwipeTime = 0.5f;
}
