using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {

    public Text ScoreText;
    public Text TokenText;

    void Start()
    {
        HUDScorePanel.Instance.SetTextObjects(ScoreText, TokenText);
    }
}
