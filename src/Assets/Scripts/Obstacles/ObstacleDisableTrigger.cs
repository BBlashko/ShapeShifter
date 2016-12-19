using UnityEngine;

public class ObstacleDisableTrigger : MonoBehaviour {

    void Start()
    {
        switch (gameObject.tag)
        {
            case mSquare:
                mShape = PlayerShape.Shape.SQUARE;
                break;
            case mRectangle:
                mShape = PlayerShape.Shape.RECTANGLE;
                break;
            case mTriangle:
                mShape = PlayerShape.Shape.TRIANGLE;
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == mPlayerTag)
        {
            if (PlayerMovement.Instance.CurrentShape != mShape)
            {
                PlayerMovement.Instance.InstantDeath();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    private PlayerShape.Shape mShape;
    private const string mSquare = "Square";
    private const string mTriangle = "Triangle";
    private const string mRectangle = "Rectangle";
    private const string mPlayerTag = "Player";
}
