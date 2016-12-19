using UnityEngine;

public class LevelScroller : MonoBehaviour {

    public Vector3 Velocity;

    //Update is called once per frame
    void Update()
    {
        gameObject.transform.position += Velocity * Time.deltaTime;
    }
}
