using UnityEngine;
using UnityEngine.UI;

public class HUDScorePanel {
    public static HUDScorePanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new HUDScorePanel();
            }
            return instance;
        }
    }

    public void Update()
    {
        if (!GamePlayManager.Instance.IsCountownEnabled())
        {
            UpdateScore();
            UpdateTokenCount();
            UpdateTime();
            UpdateDistance();
        }
    }

    public void SetObjects(Text score, Text tokens, Text[] time, Text distance, RectTransform distanceLine, GameObject distanceBar)
    {
        mScoreText = score;
        mTokenText = tokens;
        mTimeText = time;
        mDistanceText = distance;

        mDistanceBar = distanceBar;
        mDistanceLine = distanceLine;

        mDistanceBarInitalPos = mDistanceBar.transform.position;
    }

    private void UpdateScore()
    {
        if (mScoreText != null)
        {
            mScoreText.text = GamePlayManager.Instance.GetCurrentGame().Score.ToString();
        }
    }

    private void UpdateTokenCount()
    {
        if (mTokenText != null)
        {
            mTokenText.text = GamePlayManager.Instance.GetCurrentGame().Tokens.ToString();
        }
    }

    private void UpdateDistance()
    {
        float percentDistance = (100.0f - GamePlayManager.Instance.GetCurrentGame().PercentageDistanceLeft());
        mDistanceText.text = percentDistance.ToString("N0") + "%";

        float xPos = mDistanceBarInitalPos.x + ((mDistanceLine.rect.width * mDistanceLine.lossyScale.x) * (percentDistance/100.0f));
        mDistanceBar.transform.position = new Vector3(xPos, mDistanceBar.transform.position.y, mDistanceBar.transform.position.z);
    }

    private void UpdateTime()
    {
        if(mTimeText != null)
        {
            int elapsedTime = (int) GamePlayManager.Instance.GetCurrentGame().ElapsedTime;

            string [] time = HelperFunctions.ConvertTime(elapsedTime);
            mTimeText[0].text = time[0];
            mTimeText[1].text = time[1];
            mTimeText[2].text = time[2];
        }
    }

    private static HUDScorePanel instance;

    //HUD Objects
    private Text mScoreText;
    private Text mTokenText;
    private Text[] mTimeText;

    //distance object
    private GameObject mDistanceBar;
    private RectTransform mDistanceLine;
    private Text mDistanceText;
    private Vector3 mDistanceBarInitalPos;

    //Final Stats
    private int mFinalTime;
}
