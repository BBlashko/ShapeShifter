using UnityEngine;

public class Token : MonoBehaviour {

    public float speed = 150f;

    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.transform.tag == mPlayerTag && !mIsColliding)
        {
            //Since collider is a sphere it could have multiple collision points
            mIsColliding = true;

            //Update current games token count.
            HUDScorePanel.Instance.UpdateScore(100);
            HUDScorePanel.Instance.UpdateTokenCount(1);
            gameObject.SetActive(false);
            mIsColliding = false;
        }
    }

    private const string mPlayerTag = "Player";
    private bool mIsColliding = false;
}
