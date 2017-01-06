using UnityEngine.UI;
using UnityEngine;

public class HUDManager : MonoBehaviour {

    public Text ScoreText;
    public Text TokenText;

    void Start()
    {
        HUDScorePanel.Instance.SetTextObjects(ScoreText, TokenText);
    }

    void OnEnable()
    {
        ResetPanel();
    }

    void OnDisable()
    {
        ResetPanel();
    }

    public void ResetPanel()
    {
        HUDScorePanel.Instance.Reset();
    }
}
