using UnityEngine;

public class LevelScroller : MonoBehaviour {

    public Vector3 Velocity;

    //Update is called once per frame
    void Update()
    {
        gameObject.transform.position += Velocity * Time.deltaTime;
    }

    public void StopScrolling()
    {
        Velocity = new Vector3(0.0f, 0.0f, 0.0f);
    }
}
