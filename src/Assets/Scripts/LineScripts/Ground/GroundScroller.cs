using UnityEngine;

public class GroundScroller : MonoBehaviour {

    public GameObject[] GroundSections = new GameObject[2];
    public float GroundSectionLength;
    public Vector3 Velocity;

    void Start()
    {
        InitialVelocity = Velocity;
        mSectionResetPosition = new Vector3(GroundSections.Length * GroundSectionLength, 0.0f, 0.0f);
    }

    void Update()
    {

        for (int i = 0; i < GroundSections.Length; i++)
        {
            GameObject section = GroundSections[i];
            section.transform.position += Velocity * Time.deltaTime;

            if (section.transform.position.x < -GroundSectionLength * 2.0f)
            {
                if (GamePlayManager.Instance.PlayingLevel && !mFinishedStartingLevel)
                {
                    if (mStartingLevelFirstReset)
                    {
                        GamePlayManager.Instance.StartLevelScroll(Velocity);
                        section.transform.position += mSectionResetPosition;
                        mStartingLevelFirstReset = false;
                    }
                    else if (mFirstResetSectionIndex == i)
                    {
                        StopScrolling();
                        mFinishedStartingLevel = true;
                    }
                }
                else
                {
                    section.transform.position += mSectionResetPosition;
                }
            }
        }
    }

    public Vector3 GetVelocity()
    {
        return Velocity;
    }

    private void StopScrolling()
    {
        Velocity = new Vector3(0.0f, 0.0f, 0.0f);
    }

    private void StartScrolling()
    {
        Velocity = InitialVelocity;
    }

    private Vector3 InitialVelocity;
    private Vector3 mSectionResetPosition;

    private int mFirstResetSectionIndex = -1;
    private bool mFinishedStartingLevel = false;
    private bool mStartingLevelFirstReset = true;
}
