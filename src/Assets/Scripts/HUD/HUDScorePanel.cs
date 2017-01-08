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
        UpdateScore();
        UpdateTokenCount();
        UpdateTime();
        //TODO:
        //UpdateDistance();
    }

    public void SetTextObjects(Text score, Text tokens, Text[] time, GameObject distance)
    {
        mScoreText = score;
        mTokenText = tokens;
        mTimeText = time;
        mDistanceGameObject = distance;
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
    private GameObject mDistanceGameObject;

    //Final Stats
    private int mFinalTime;
}
