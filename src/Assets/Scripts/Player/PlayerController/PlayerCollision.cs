using UnityEngine;

public class PlayerCollision {

    public static PlayerCollision Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerCollision();
            }
            return instance;
        }
    }

    public void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.transform.tag == TagManager.Ground)
        {
            PlayerMovement.Instance.GroundCollisionStay(collisionInfo);
        }

        if (collisionInfo.gameObject.transform.tag == TagManager.LeftBoundary)
        {
            //Resets Left Boundary Condition
            PlayerMovement.Instance.IsAgainstLeftBoundary = true;
        }
        else if (collisionInfo.gameObject.layer == mDeathLayer)
        {
            //Triggers Player Death
            switch (collisionInfo.gameObject.tag)
            {
                case TagManager.Obstacle:
                    PlayerMovement.Instance.InstantDeath();
                    break;
                case TagManager.Square:
                    PlayerMovement.Instance.CheckDeath(PlayerShape.Shape.SQUARE);
                    break;
                case TagManager.Rectangle:
                    PlayerMovement.Instance.CheckDeath(PlayerShape.Shape.RECTANGLE);
                    break;
                case TagManager.Triangle:
                    PlayerMovement.Instance.CheckDeath(PlayerShape.Shape.TRIANGLE);
                    break;
                default:
                    break;
            }
        }

        if (collisionInfo.gameObject.transform.tag == TagManager.LevelComplete)
        {
            GamePlayManager.Instance.LevelCompleted();
        }
    }

    public void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.transform.tag == TagManager.LeftBoundary)
        {
            PlayerMovement.Instance.IsAgainstLeftBoundary = false;
        }
        else if (collisionInfo.gameObject.transform.tag == TagManager.Ground)
        {
            PlayerMovement.Instance.GroundCollisionExit(collisionInfo);
        }
    }

    public void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.transform.tag == TagManager.Ground)
        {
            PlayerMovement.Instance.GroundCollisionStay(collisionInfo);
        }
    }

    //Class instance
    private PlayerCollision() { }
    private static PlayerCollision instance;

    //GameObject Layers
    private const int mDeathLayer = 10;
}
