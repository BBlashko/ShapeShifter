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
        if (collisionInfo.gameObject.transform.tag == mLeftBoundaryTag)
        {
            Debug.Log("CollisionEntered");
            PlayerMovement.Instance.IsAgainstLeftBoundary = true;
        }
    }

    public void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.transform.tag == mLeftBoundaryTag)
        {
            Debug.Log("CollisionExit");
            PlayerMovement.Instance.IsAgainstLeftBoundary = false;
        }
    }

    public void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.transform.tag == mGroundTag)
        {
            PlayerMovement.Instance.GroundCollision(collisionInfo);
        }

    }

    //Class instance
    private PlayerCollision() { }
    private static PlayerCollision instance;

    //GameObject Tags
    private const string mGroundTag = "Ground";
    private const string mLeftBoundaryTag = "LeftBoundary";
}
