using UnityEngine.UI;
using UnityEngine;

public class HUDManager : MonoBehaviour {

    public Text ScoreText;
    public Text TokenText;
    public Text[] TimeText;
    public GameObject DistanceGameObject;

    //TODO:
    //public GameObject DistanceObject
    //public Text TimeText;

    void Start()
    {
        HUDScorePanel.Instance.SetTextObjects(ScoreText, TokenText, TimeText, DistanceGameObject);
    }

    void Update()
    {
        HUDScorePanel.Instance.Update();
    }
}
