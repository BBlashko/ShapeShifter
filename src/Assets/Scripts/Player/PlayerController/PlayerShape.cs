using UnityEngine;

public class PlayerShape {

    public enum Shape { RECTANGLE, SQUARE, TRIANGLE }
    public Shape CurrentShape;

    public void Instantiate(GameObject player)
    {
        mSquareShape = player.transform.FindChild(mSquareObjectName).gameObject;
        mTriangleShape = player.transform.FindChild(mTriangleObjectName).gameObject;
        mRectangleShape = player.transform.FindChild(mRectangleObjectName).gameObject;

        mLeftBoundary = GameObject.Find(mLeftBoundaryName);
    }

    public void ChangeShape(Shape shape)
    {
        if (CurrentShape != shape)
        {
            DisableCurrentShape();
            switch (shape)
            {
                case Shape.SQUARE:
                    mSquareShape.SetActive(true);
                    break;
                case Shape.RECTANGLE:
                    mLeftBoundary.transform.position = new Vector3(-6.117f,
                                                                   mLeftBoundary.transform.position.y,
                                                                   mLeftBoundary.transform.position.z);
                    mRectangleShape.SetActive(true);
                    break;
                case Shape.TRIANGLE:
                    mTriangleShape.SetActive(true);
                    break;
            }
            CurrentShape = shape;
        }
    }

    public void EnableParticles()
    {
        mSquareShape.GetComponentInChildren<ParticleSystem>().Play();
        mRectangleShape.GetComponentInChildren<ParticleSystem>().Play();
        mTriangleShape.GetComponentInChildren<ParticleSystem>().Play();
    }

    public void DisableParticles()
    {
        mSquareShape.GetComponentInChildren<ParticleSystem>().Stop();
        mRectangleShape.GetComponentInChildren<ParticleSystem>().Stop();
        mTriangleShape.GetComponentInChildren<ParticleSystem>().Stop();
    }

    private void DisableCurrentShape()
    {
        switch (CurrentShape)
        {
            case Shape.SQUARE:
                mSquareShape.SetActive(false);
                break;
            case Shape.RECTANGLE:
                mLeftBoundary.transform.position = new Vector3(-5.5f,
                                                                  mLeftBoundary.transform.position.y,
                                                                  mLeftBoundary.transform.position.z);
                mRectangleShape.SetActive(false);
                break;
            case Shape.TRIANGLE:
                mTriangleShape.SetActive(false);
                break;
            default:
                break;
        }
    }

    //Prefabs
    private GameObject mSquareShape;
    private GameObject mTriangleShape;
    private GameObject mRectangleShape;

    //Resource Path Strings
    private string mSquareObjectName = "SquareMesh";
    private string mTriangleObjectName = "TriangleMesh";
    private string mRectangleObjectName = "RectangleMesh";

    //LeftBoundary Gameobject
    //Needed for when the shape is changed to a rectangle
    private GameObject mLeftBoundary;
    private const string mLeftBoundaryName = "LeftBoundary";
   
}
