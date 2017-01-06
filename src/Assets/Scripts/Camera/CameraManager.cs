using UnityEngine;

public class CameraManager : MonoBehaviour {

    public GameObject Player;
    public float SmoothSpeedX;
    public float SmoothSpeedY;

    void Start()
    {
        mMinYPosition = transform.position.y;
        mPlayerXOffset = transform.position.x - Player.transform.position.x;
    }

	// Update is called once per frame
	void Update () {

        //if playing move camera with player
        if (GamePlayManager.Instance.PlayingLevel)
        {
            float positionX = Mathf.SmoothDamp(transform.position.x, Player.transform.position.x + mPlayerXOffset, ref velocity.x, SmoothSpeedX);
            float positionY = Mathf.SmoothDamp(transform.position.y, Player.transform.position.y, ref velocity.y, SmoothSpeedY);

            if (positionY < mMinYPosition)
            {
                positionY = mMinYPosition;
            }
            transform.position = new Vector3(positionX, positionY, transform.position.z);
        }
	}

    private float mMinYPosition;
    private float mPlayerXOffset;

    private Vector3 velocity;
}
