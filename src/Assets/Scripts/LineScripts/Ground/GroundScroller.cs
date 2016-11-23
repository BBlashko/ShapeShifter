using UnityEngine;

public class GroundScroller : MonoBehaviour {

    public GameObject[] GroundSections = new GameObject[2];
    public Vector3 Velocity;

    //Update is called once per frame
    void Update()
    {
        foreach (GameObject section in GroundSections)
        {
            section.transform.position += Velocity * Time.deltaTime;
            if (section.transform.position.x < (-9.025 * 2))
            {
                section.transform.position += new Vector3(36.0f, 0.0f, 0.0f);
            }
        }
    }
}
