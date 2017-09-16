using UnityEngine;

public class GroundScroller : MonoBehaviour {

    public GameObject[] GroundSections = new GameObject[2];
    public float GroundSectionLength;
    public Vector3 Velocity;

    void Start()
    {
        mStartingPosition = GroundSections[0].transform.position;
        InitialVelocity = Velocity;
        mSectionResetPosition = new Vector3(GroundSections.Length * GroundSectionLength, 0.0f, 0.0f);
    }

    void Update()
    {
        for (int i = 0; i < GroundSections.Length; i++)
        {
            GameObject section = GroundSections[i];
            section.transform.position += Velocity * Time.deltaTime;

            if (GamePlayManager.Instance.PlayingLevel)
            {
                //PlayingLevel is true, and the ground should be disabled and stopped scrolling
                StopScrolling();
                SetActive(false);
                continue;
            }
            else if(section.transform.position.x < -GroundSectionLength * 2.0f)
            {
                //PlayingLevel is false and ground has been reset and needs to be scrolled.
                section.transform.position += mSectionResetPosition;
            }            
        }
    }

    public Vector3 GetVelocity()
    {
        return Velocity;
    }

    public void SetActive(bool b)
    {
        this.gameObject.SetActive(b);
    }

    public void StopScrolling()
    {
        SetActive(false);
        Velocity = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void StartScrolling()
    {
        SetActive(true);
        Velocity = InitialVelocity;
    }

    public void ResetGround()
    {
        for (int i = 0; i < GroundSections.Length; i++)
        {
            GameObject section = GroundSections[i];
            section.transform.position = new Vector3(mStartingPosition.x + (GroundSectionLength * i), mStartingPosition.y, mStartingPosition.z);
        }

        StartScrolling();
    }

    private Vector3 InitialVelocity;
    private Vector3 mSectionResetPosition;
    private Vector3 mStartingPosition;
}
