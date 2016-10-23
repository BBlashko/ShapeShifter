using UnityEngine;
using System.Collections.Generic;

public class GroundLines : MonoBehaviour {

    public GameObject linePrefab;
    public float xSpeed;
    public float y1;
    public float y2;

    void Start () {
        GenerateLines();
    }

    void Update () {
        if (mLines != null)
        {
            for (int i = 0; i < mLines.Count; i++)
            {
                LineScroller lineScroller = mLines[i].GetComponent<LineScroller>();
                mLines[i].GetComponent<LineRenderer>().SetPositions(lineScroller.UpdateLinePositions());
            }
        }
    }

    private void GenerateLines()
    {
        float height = Camera.main.orthographicSize * 2.0f;
        float screenWidth = height * Screen.width / Screen.height;
        float screenLeftXPosition = 0 - (screenWidth / 2);
        float lineHeight = Mathf.Abs(y1 - y2);
        int numberOfLines = (int) Mathf.Ceil(screenWidth / lineHeight);

        mLines = new List<GameObject>();

        for (int i = 0; i < numberOfLines; i++)
        {
            GameObject line = Instantiate(linePrefab);
            line.transform.parent = gameObject.transform;
            LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
            Vector3[] linePositions = { new Vector3(screenLeftXPosition + (i * lineHeight), y1, 0), new Vector3(screenLeftXPosition + (i * lineHeight), y2, 0) };
            lineRenderer.SetPositions(linePositions);

            LineScroller lineScroller = line.AddComponent<LineScroller>();
            lineScroller.Init(xSpeed, linePositions, numberOfLines * lineHeight);
            mLines.Add(line);
        }
    }

    private List<GameObject> mLines;
}
