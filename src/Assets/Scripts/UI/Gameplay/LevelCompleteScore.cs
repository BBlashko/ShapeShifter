using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteScore : MonoBehaviour {

    public Text ScoreText;
    public Text TokenText;
    public Text[] TimeText;

    public GameObject [] Stars;
    public GameObject[] StarFull;
    public GameObject [] StarEmpty;

    void Start() {
        mStarAnimators = new Animator[Stars.Length];
        for (int i = 0; i < mStarAnimators.Length; i++)
        {
            mStarAnimators[i] = Stars[i].GetComponentInChildren<Animator>();
            mStarAnimators[i].enabled = false;
        }
    }

    void OnEnable()
    {
        //Check stats for each star obtained and play animation for each.
       
        StartCoroutine(AnimateStars());

        ScoreText.text = GamePlayManager.Instance.GetCurrentGame().Score.ToString();
        TokenText.text = GamePlayManager.Instance.GetCurrentGame().Tokens.ToString();

        string[] time = HelperFunctions.ConvertTime((int) GamePlayManager.Instance.GetCurrentGame().FinalTime);
        TimeText[0].text = time[0];
        TimeText[1].text = time[1];
        TimeText[2].text = time[2];
    }

    void OnDisable()
    {
        //reset animator for next game
        ResetPlayedOnce();
    }

   private IEnumerator AnimateStars()
    {
        yield return new WaitForSeconds(0.100F);

        if (GamePlayManager.Instance.GetCurrentGame().HasFirstStar)
        {
            mStarAnimators[0].enabled = true;
            yield return new WaitForSeconds(0.500F);
        }

        if (GamePlayManager.Instance.GetCurrentGame().HasSecondStar)
        {
            mStarAnimators[1].enabled = true;
            yield return new WaitForSeconds(0.500F);
        }

        if (GamePlayManager.Instance.GetCurrentGame().HasThirdStar)
        {
            mStarAnimators[2].enabled = true;
        }
    }

    private void ResetPlayedOnce()
    {
        Debug.Log("animations reset");
        for (int i = 0; i < mStarAnimators.Length; i++)
        {
            StarFull[i].gameObject.SetActive(false);
            StarEmpty[i].gameObject.SetActive(true);
            mStarAnimators[i].enabled = false;
        }
    }

    private Animator[] mStarAnimators;
}
