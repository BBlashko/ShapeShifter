using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    void Start()
    {
        PlayerMovement.Instance.MyMonoBehaviour = this;
        PlayerMovement.Instance.EnableDefaultShape();
    }

    void FixedUpdate()
    {
        PlayerMovement.Instance.Update();
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        PlayerCollision.Instance.OnCollisionEnter(collisionInfo);
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        PlayerCollision.Instance.OnCollisionExit(collisionInfo);
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        PlayerCollision.Instance.OnCollisionStay(collisionInfo);
    }

    void Destroy()
    {
        //Destroy player
    }
}