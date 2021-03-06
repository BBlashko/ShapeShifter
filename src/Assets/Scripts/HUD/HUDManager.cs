﻿using UnityEngine.UI;
using UnityEngine;

public class HUDManager : MonoBehaviour {

    public Text ScoreText;
    public Text TokenText;
    public Text[] TimeText;
    public Text DistanceText;

    public GameObject DistanceBar;
    public RectTransform DistanceLine;    

    void Start()
    {
        HUDScorePanel.Instance.SetObjects(ScoreText, TokenText, TimeText, DistanceText, DistanceLine, DistanceBar);
    }

    void Update()
    {
        HUDScorePanel.Instance.Update();
    }
}
