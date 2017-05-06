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
        mTotalDistance = GameObject.Find(levelCompletePath).transform.position.x; ;

        mTokens = 0;
        mScore = 0;
        mStartTime = Time.time;

        //Reset Stars
        HasFirstStar = false;
        HasSecondStar = false;
        mHasThirdStar = false;

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
        float retval = (mPlayer.transform.position.x - mPlayerStartPositionX) / mTotalDistance;
        if (retval > 100.0f)
        {
            return 100.0f;
        }
        return retval;
    }

    //Player pointer
    private const string mPlayerName = "player";
    private GameObject mPlayer;

    //Level info
    private const string mPlayerLevelCompleteName = "LevelComplete";
    private const string levelCompleteGameObjectName = "/Platforms/LevelComplete";
    private float mTotalDistance;

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
}
