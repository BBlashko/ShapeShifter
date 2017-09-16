using UnityEngine;

public class CurrentGame : MonoBehaviour
{
    public int ScoreRule;
    public int TimeRule;
    public int TokenRule;
    public int LevelId;

    public void StartGame()
    {
        mPlayer = GameObject.Find(mPlayerName);
        string levelCompletePath = "GamePlay/Level" + LevelId.ToString() + "(Clone)" + levelCompleteGameObjectName;
        Debug.Log(levelCompletePath);
        mCompletePlatform = GameObject.Find(levelCompletePath);
        mTotalDistance = mCompletePlatform.transform.position.x - mPlayer.transform.position.x;
        mCompletePlatformWidth = mCompletePlatform.GetComponentInChildren<BoxCollider>().bounds.size.x;

        mTokens = 0;
        mScore = 0;
        mStartTime = Time.time;

        //Reset Stars
        HasFirstStar = false;
        HasSecondStar = false;
        mHasThirdStar = false;

        Debug.Log("Start  " + mStartTime);
    }

    public void PauseGame()
    {
        mPausedStartElapsed = ElapsedTime;
        mIsPaused = true;
        mPausedTimeStart = Time.time;
    }

    public void ResumeGame()
    {
        mIsPaused = false;
        mPausedTime = Time.time - mPausedTimeStart;
    }

    public bool IsPaused
    {
        get { return mIsPaused; }
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
        get
        {
            if (!mIsPaused)
            {
                return Time.time - mStartTime - mPausedTime;
            }
            else
            {
                return mPausedStartElapsed;
            }
        }
    }

    public bool HasFirstStar
    {
        get { return mHasFirstStar; }
        set { mHasFirstStar = value; }
    }

    public bool HasSecondStar
    {
        get { return mHasSecondStar; }
        set { mHasSecondStar = value; }
    }

    public bool HasThirdStar
    {
        get { return mHasThirdStar; }
        set { mHasThirdStar = value; }
    }

    public float FinalTime
    {
        get { return mFinalTime; }
        set { mFinalTime = value; }
    }

    public void SetStars()
    {
        //First star
        Debug.Log(FinalTime + " : " + TimeRule);
        if (FinalTime <= TimeRule)
        {
            HasFirstStar = true;
        }

        //Second star
        Debug.Log(Score + " : " + ScoreRule);
        if (Score >= ScoreRule)
        {
            HasSecondStar = true;
        }

        //Thirdstar
        Debug.Log(Tokens + " : " + TokenRule);
        if (Tokens >= TokenRule)
        {
            HasThirdStar = true;
        }
    }

    public float PercentageDistanceLeft()
    {
        if (mCompletePlatform != null)
        {
            float retval = (((mCompletePlatform.transform.position.x - (mCompletePlatformWidth / 2)) - mPlayer.transform.position.x) / mTotalDistance) * 100;
            if (retval < 0.0f)
            {
                return 0.0f;
            }
            else if (retval > 100.0f)
            {
                return 100.0f;
            }
            return retval;
        }
        return 100.0f;
    }

    public Vector3 PlayerPosition
    {
        get { return mPlayerPosition; }
        set { mPlayerPosition = value; }
    }

    public Vector3 LevelPosition
    {
        get { return mLevelPosition; }
        set { mLevelPosition = value; }
    }

    public void TutorialRespawnPlayer()
    {
        PlayerMovement.Instance.Respawn(mPlayerPosition);
        gameObject.transform.position = mLevelPosition;
    }

    //Player pointer
    private const string mPlayerName = "GamePlay/Player";
    private GameObject mPlayer;

    //Level info
    private const string mPlayerLevelCompleteName = "LevelComplete";
    private const string levelCompleteGameObjectName = "/Platforms/LevelComplete";
    private float mTotalDistance;
    private GameObject mCompletePlatform;
    private float mCompletePlatformWidth;

    //Current game stats
    private int mTokens;
    private int mScore;
    private float mStartTime;
    private float mPlayerStartPositionX;
    private float mFinalTime;

    //Stars
    private bool mHasFirstStar = false;
    private bool mHasSecondStar = false;
    private bool mHasThirdStar = false;

    //Game State
    private bool mIsPaused;
    private float mPausedTime = 0;
    private float mPausedStartElapsed;
    private float mPausedTimeStart;

    //Tutorial Player/level location for respawn
    private Vector3 mPlayerPosition;
    private Vector3 mLevelPosition;
}
