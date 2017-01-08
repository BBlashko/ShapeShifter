using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteScore : MonoBehaviour {

    public Text ScoreText;
    public Text TokenText;
    public Text[] TimeText;

    public GameObject [] Stars;
    public GameObject [] StarEmpty;

    void Start() {
        mStarAnimators = new Animator[Stars.Length];
        for (int i = 0; i < mStarAnimators.Length; i++)
        {
            mStarAnimators[i] = Stars[i].GetComponentInChildren<Animator>();
            mStarAnimators[i].enabled = false;
        }
    }

    int k = 100;

    void Update()
    {
        if (k == 0)
        {
            AnimateStar(0);
            AnimateStar(1);
            AnimateStar(2);
        }
        k--;


    }

    void OnEnable()
    {
        //Check stats for each star obtained and play animation for each.
        /*
         * if (GamePlayManager.Instance.CurrentGame.IsFirstStars())
         * {
         *      AnimateStar(1);
         * }
         * 
         * if (GamePlayManager.Instance.CurrentGame.IsSecondStars())
         * {
         *      AnimateStar(2);
         * }
         * 
         * if (GamePlayManager.Instance.CurrentGame.IsThirdStars())
         * {
         *      AnimateStar(3);
         * }
         */


        ScoreText.text = GamePlayManager.Instance.GetCurrentGame().Score.ToString();
        TokenText.text = GamePlayManager.Instance.GetCurrentGame().Tokens.ToString();

        string[] time = HelperFunctions.ConvertTime((int) GamePlayManager.Instance.GetCurrentGame().GetFinalTime());
        TimeText[0].text = time[0];
        TimeText[1].text = time[1];
        TimeText[2].text = time[2];
    }

    void OnDisable()
    {
        //reset animator for next game
        ResetPlayedOnce();
    }

   private void AnimateStar(int i)
    {
        //StarEmpty[i].SetActive(false);
        //Stars[i].SetActive(true);
        if (mStarAnimators[i].enabled == false)
        {
            mStarAnimators[i].enabled = true;
        }

        //Check that previous star is not currently animating
        //mStarAnimators[i].SetBool("PlayedOnce", false);//
        //mStarAnimators[i].SetTrigger("PlayAnimation");
        //while (mStarAnimators[i].GetCurrentAnimatorStateInfo(0).IsName("idle_state")) { };
    }

    private void ResetPlayedOnce()
    {
        for (int i = 0; i < mStarAnimators.Length; i++)
        {
            mStarAnimators[i].enabled = false;
        }

    }

    private Animator[] mStarAnimators;
    private bool[] mAnimatorPlayedOnce = {false, false, false};

}
