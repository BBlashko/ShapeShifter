using UnityEngine;
using System.Collections.Generic;

public class BackgroundLines : MonoBehaviour {

	public GameObject linePrefab;
    public float xSpeed;
    public int numSegments;

    void Start () {
        mScreenHeight = Camera.main.orthographicSize * 2.0f;
        mScreenWidth = mScreenHeight * Screen.width / Screen.height;

        InstantiateChildGameObject("Lines");
        SetLinePositions();
        GenerateLineRenderers();
    }

    void Update()
    {
        if (mLineObjects != null && mIsScrolling)
        {
            for (int i = 0; i < mLineObjects.Count; i++)
            {
                LineScroller lineScroller = mLineObjects[i].GetComponent<LineScroller>();
                LineRenderer lineRenderer = mLineObjects[i].GetComponent<LineRenderer>();
                Vector3[] nextPositions = lineScroller.UpdateLinePositions();
                lineRenderer.SetPositions(nextPositions);
                CheckToDisableLineRenderer(i, nextPositions);
            }
        }
    }

    public void StartScrolling()
    {
        mIsScrolling = true;
    }

    public void StopScrolling()
    {
        mIsScrolling = false;
    }

    private void SetLinePositions()
    {
        //line coords
        mNumVertices = 5 * numSegments;
        mLineVertices = new Vector3[mNumVertices];
        float maxHeight = mScreenHeight / 3.5f;

        mSegmentSize = ((mScreenWidth / 2.0f) / (numSegments - 2)) * 2.0f;
        int index = 0;
        float currentX = -(mSegmentSize);
        float yCoord = RandomLineHeight(maxHeight);
        float firstY = yCoord;
        for (int i = 0; i < numSegments; i++)
        {
            mLineVertices[index++] = new Vector3(currentX, yCoord, 0.0f);
            currentX += mSegmentSize / 2.0f;
            mLineVertices[index++] = new Vector3(currentX, yCoord, 0.0f);
            yCoord = -RandomLineHeight(maxHeight);
            mLineVertices[index++] = new Vector3(currentX, yCoord, 0.0f);
            currentX += mSegmentSize / 2.0f;
            mLineVertices[index++] = new Vector3(currentX, yCoord, 0.0f);
            yCoord = RandomLineHeight(maxHeight);
            if (index == mNumVertices - 1)
            {
                mLineVertices[index++] = new Vector3(currentX, firstY, 0.0f);
            }
            else
            {
                mLineVertices[index++] = new Vector3(currentX, yCoord, 0.0f);
            }
        }
    }

    private void GenerateLineRenderers()
    {
        mLineObjects = new List<GameObject>();
        for (int i = 0; i < mNumVertices; i++)
        {
            GameObject line = Instantiate(linePrefab);
            line.transform.parent = mLineGameObject.transform;
            line.transform.position = mLineGameObject.transform.position;

            LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
            Vector3[] linePositions = { mLineVertices[i % mNumVertices], mLineVertices[(i + 1) % mNumVertices] };
            lineRenderer.SetPositions(linePositions);
            LineScroller lineScroller = line.AddComponent<LineScroller>();
            lineScroller.Init(xSpeed, linePositions, mSegmentSize * 2.0f + mScreenWidth, -mSegmentSize, mScreenWidth);
            mLineObjects.Add(line);
        }
    }

    private void CheckToDisableLineRenderer(int index, Vector3[] nextPositions)
    {
        if (nextPositions[1].x < -mSegmentSize / 2.0f)
        {
            mLineObjects[index].SetActive(false);
        }
        else if (!mLineObjects[index].activeInHierarchy)
        {
            mLineObjects[index].SetActive(true);
        }
    }

    private float RandomLineHeight(float maxHeight)
    {
        return Random.Range(0.5f, maxHeight);
    }

    private void InstantiateChildGameObject(string name)
    {
        mLineGameObject = new GameObject();
        mLineGameObject.transform.parent = gameObject.transform;
        mLineGameObject.name = name;
        mLineGameObject.transform.position = new Vector3(-mScreenWidth / 2.0f, 0.0f, 3.0f);
    }

    //screen
    private float mScreenWidth;
    private float mScreenHeight;

    //line objects
    private GameObject mLineGameObject;
    private Vector3[] mLineVertices;
    private List<GameObject> mLineObjects;
    private float mSegmentSize;
    private int mNumVertices;

    //Start/Stop Scrolling
    private bool mIsScrolling = true;
}
