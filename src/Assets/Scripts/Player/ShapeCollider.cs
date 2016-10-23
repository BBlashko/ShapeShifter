using UnityEngine;
using System.Collections;

public class ShapeCollider : MonoBehaviour {

    void OnCollisionStay(Collision collisionInfo)
    {
        Debug.Log("onCollisionStay");
        //mIsGrounded = true;
        //mCanDoubleJump = true;
    }
}
