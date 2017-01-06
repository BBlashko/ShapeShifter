using UnityEngine;

public class LineScroller : MonoBehaviour
{

    public void Init(float xSpeed, Vector3[] startingLinePositions, float resetX, float leftXResetTrigger, float width)
    {
        mScreenWidth = width;
        mScreenLeftXPosition = leftXResetTrigger;
        mXSpeed = xSpeed;
        mCurrentLinePositions = startingLinePositions;
        mResetX = resetX;
    }

    public Vector3[] UpdateLinePositions()
    {
        for (int i = 0; i < mCurrentLinePositions.Length; i++)
        {
            float xPosition = mCurrentLinePositions[i].x + mXSpeed;
            if (xPosition <= mScreenLeftXPosition)
            {
                xPosition += mResetX;
            }
            mCurrentLinePositions[i] = new Vector3(xPosition, mCurrentLinePositions[i].y, mCurrentLinePositions[i].z);
        }
        return mCurrentLinePositions;
    }

    private float mScreenLeftXPosition;
    private float mScreenWidth;
    private float mXSpeed;
    private Vector3[] mCurrentLinePositions;
    private float mResetX;
}