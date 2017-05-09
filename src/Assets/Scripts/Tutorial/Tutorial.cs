using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    public List<GameObject> TutorialScreens;
    public GameObject mTutorialCanvas;
    public GameObject Level;

    void Start()
    {
        //Show Tutorial Canvas
        mTutorialCanvas = HelperFunctions.FindInactiveGameObject("TutorialCanvas");
        MenuManager.Instance.DisableCurrentMenu();
        mTutorialCanvas.SetActive(true);
        TutorialScreens[mScreenIndex].SetActive(true);
        mPlayerGameObject = GameObject.Find("Player");
    }

    void Update()
    {
        if (mScreenIndex >= mTutorialLevelStartIndex)
        {
            //Poll for Percentage increase. 
            float percentage = 100.0f - GamePlayManager.Instance.GetCurrentGame().PercentageDistanceLeft();
            if (mCurrTriggerIndex < mDescriptionTriggers.Count && percentage >= mDescriptionTriggers[mCurrTriggerIndex])
            {
                Debug.Log("STOPPING TUTORIAL1!!!!");
                mCurrTriggerIndex++;
                GamePlayManager.Instance.PauseGame();
                NextTutorialScreen();
            }
        }
    }

    //Buttons
    public void SkipTutorial()
    {
        mTutorialCanvas.SetActive(false);
        GamePlayManager.Instance.SkipTutorial();
    }

    //public void PreviousTutorialScreen()
    //{
    //    if (mScreenIndex >= mTutorialLevelStartIndex)
    //    {
    //        return;
    //    }

    //    TutorialScreens[mScreenIndex--].SetActive(false);
    //    Debug.Log("previousScreen : " + mScreenIndex);
    //    TutorialScreens[mScreenIndex].SetActive(true);
    //}

    public void NextTutorialScreen()
    {
        TutorialScreens[mScreenIndex++].SetActive(false);
        Debug.Log("Next Screen : " + mScreenIndex);
        GamePlayManager.Instance.GetCurrentGame().PlayerPosition = mPlayerGameObject.transform.position;
        GamePlayManager.Instance.GetCurrentGame().LevelPosition = gameObject.transform.position;

        if (mScreenIndex > TutorialScreens.Count - 2)
        {
            mScreenIndex = TutorialScreens.Count - 2;
            return;
        }

        

        if (mScreenIndex == mTutorialLevelStartIndex)
        {
            //call when ready to start level
            GamePlayManager.Instance.StartGame();
            TutorialScreens[mScreenIndex].SetActive(true);
        }
        else
        {
            TutorialScreens[mScreenIndex].SetActive(true);
        }

    }

    public void ResumeTutorialLevel()
    {
        GamePlayManager.Instance.ResumeGame();
        NextTutorialScreen();
    }

    private int mScreenIndex = 0;
    private const int mTutorialLevelStartIndex = 6;

    private static List<float> mDescriptionTriggers = new List<float>{2.0f, 5.5f, 47.5f, 60.5f, 69.7f, 81.5f};
    private int mCurrTriggerIndex = 0;

    private GameObject mPlayerGameObject;
}
