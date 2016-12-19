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
        Debug.Log("GameObject name on collision enter = " + collisionInfo.gameObject.name);
        if (collisionInfo.gameObject.transform.tag == TagManager.LeftBoundary)
        {
            //Resets Left Boundary Condition
            Debug.Log("CollisionEntered");
            PlayerMovement.Instance.IsAgainstLeftBoundary = true;
        }
        else if (collisionInfo.gameObject.transform.tag == TagManager.Ground)
        {
            //Emits the players ground particles
            PlayerMovement.Instance.EnableParticles();
        }
        else if (collisionInfo.gameObject.layer == mDeathLayer)
        {
            Debug.Log("Death Layer!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            //Triggers Player Death
            switch (collisionInfo.gameObject.tag)
            {
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
            
            //TODO: Add level ending function call
            //TODO: Call menu activation
        }
    }

    public void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.transform.tag == TagManager.LeftBoundary)
        {
            Debug.Log("CollisionExit");
            PlayerMovement.Instance.IsAgainstLeftBoundary = false;
        }
        else if (collisionInfo.gameObject.transform.tag == TagManager.Ground)
        {
            PlayerMovement.Instance.SetGravity(true);
            PlayerMovement.Instance.DisableParticles();
        }
    }

    public void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.transform.tag == TagManager.Ground)
        {
            PlayerMovement.Instance.GroundCollision(collisionInfo);
        }

    }

    //Class instance
    private PlayerCollision() { }
    private static PlayerCollision instance;

    //GameObject Layers
    private const int mDeathLayer = 10;
}
