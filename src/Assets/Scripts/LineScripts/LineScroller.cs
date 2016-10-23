using UnityEngine;

public class LineScroller : MonoBehaviour {

    public void Init(float xSpeed, Vector3[] startingLinePositions, float totalWidthOfLines)
    {
        float height = Camera.main.orthographicSize * 2.0f;
        mScreenWidth = height * Screen.width / Screen.height; 
        mScreenLeftXPosition = 0 - (mScreenWidth / 2);
        mXSpeed = xSpeed;
        mCurrentLinePositions = startingLinePositions;
        mTotalWidthOfLines = totalWidthOfLines;
    }

    public Vector3[] UpdateLinePositions()
    {
        for (int i = 0; i < mCurrentLinePositions.Length; i++)
        {
            float xPosition = mCurrentLinePositions[i].x + mXSpeed;
            if (xPosition <= mScreenLeftXPosition)
            {
                xPosition += mTotalWidthOfLines;
            }
            mCurrentLinePositions[i] = new Vector3(xPosition, mCurrentLinePositions[i].y, mCurrentLinePositions[i].z);
        }
        return mCurrentLinePositions;
    }

    private float mScreenLeftXPosition;
    private float mScreenWidth;
    private float mXSpeed;
    private Vector3[] mCurrentLinePositions;
    private float mTotalWidthOfLines;
}
