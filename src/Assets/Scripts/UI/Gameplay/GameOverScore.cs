using UnityEngine;
using UnityEngine.UI;

public class GameOverScore : MonoBehaviour {

    public Text ScoreText;
    public Text TokenText;
    public Text[] TimeText;

    // Use this for initialization
    void OnEnable() {
        ScoreText.text = GamePlayManager.Instance.GetCurrentGame().Score.ToString();
        TokenText.text = GamePlayManager.Instance.GetCurrentGame().Tokens.ToString();

        int elapsedTime = (int)GamePlayManager.Instance.GetCurrentGame().GetFinalTime();
        string[] time = HelperFunctions.ConvertTime(elapsedTime);
        TimeText[0].text = time[0];
        TimeText[1].text = time[1];
        TimeText[2].text = time[2];
    }

    void OnDisable()
    {
        ScoreText.text = "0";
        TokenText.text = "0";
    }
}
