using UnityEngine.UI;
using System.Collections;

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

    public void SetTextObjects(Text score, Text tokens)
    {
        ScoreText = score;
        TokenText = tokens;
        Reset();
    }

    public void Reset()
    {
        if (ScoreText != null && TokenText != null)
        {
            ScoreText.text = "0";
            TokenText.text = "0";
        }
    }

    public void UpdateScore(int score)
    {
        if (ScoreText != null)
        {
            mScoreCount += score;
            ScoreText.text = mScoreCount.ToString();
        }
    }

    public void UpdateTokenCount(int count)
    {
        if (TokenText != null)
        {
            mTokenCount += count;
            TokenText.text = mTokenCount.ToString();
        }
    }

    private static HUDScorePanel instance;
    private Text ScoreText;
    private Text TokenText;
    private int mScoreCount = 0;
    private int mTokenCount = 0;
}
