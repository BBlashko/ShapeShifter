using UnityEngine;

public class CurrentGame {


	public CurrentGame(float levelTotalDistance)
    {
        mPlayer = GameObject.Find(mPlayerName);
        mTotalDistance = levelTotalDistance;
    }

    public void StartGame()
    {
       // mPlayerStartPositionX = mPlayer.transform.position.x;
        mTokens = 0;
        mScore = 0;
        mStartTime = Time.time;
        Debug.Log("Start  " + mStartTime);
    }

    public int Tokens
    {
        get { return mTokens; }
        set { mTokens += value; }
    }

    public int Score
    {
        get { return mScore; }
        set { mScore += value; }
    }

    public float ElapsedTime
    {
        get { return Time.time - mStartTime; }
    }

    public void SetFinalTime()
    {
        mFinalTime = ElapsedTime;
    }

    public float GetFinalTime()
    {
        return mFinalTime;
    }

    public float PercentageDistanceLeft()
    {
        float retval = (mPlayer.transform.position.x - mPlayerStartPositionX) / mTotalDistance;
        if (retval > 100.0f)
        {
            return 100.0f;
        }
        return retval;
    }

    private const string mPlayerName = "player";
    private GameObject mPlayer;

    private const string mPlayerLevelCompleteName = "LevelComplete";
    private float mTotalDistance;

    private int mTokens;
    private int mScore;
    private float mStartTime;
    private float mPlayerStartPositionX;
    private float mFinalTime;
}
