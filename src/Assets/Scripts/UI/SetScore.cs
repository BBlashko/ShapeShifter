using UnityEngine;
using UnityEngine.UI;

public class SetScore : MonoBehaviour {

    public Text ScoreText;
    public Text TokenText;

    // Use this for initialization
    void OnEnable() {
        ScoreText.text = HUDScorePanel.Instance.GetScore().ToString();
        TokenText.text = HUDScorePanel.Instance.GetToken().ToString();
    }

    void OnDisable()
    {
        ScoreText.text = "0";
        TokenText.text = "0";
    }
}
