﻿using UnityEngine;

public class GamePlayManager
{

    public static GamePlayManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GamePlayManager();
            }
            return instance;
        }
    }

    public void SkipTutorial()
    {
        if (mCurrentLevelId == 0)
        {
            DestroyCurrentLevel();
            DestroyCurrentGame();
            LoadLevel(1);
            StartGame();
            mLevelScroller.StartScrolling();
            mGroundScroller.StartScrolling();
            mBackgroundScroller.StartScrolling();
        }
    }

    public void LoadLevel(int id)
    {
        if (!PlayingLevel)
        {
            mCurrentLevelId = id;
            mCurrentLevel = GameObject.Instantiate(Resources.Load(prefabPath + id.ToString(), typeof(GameObject))) as GameObject;
            mCurrentLevel.transform.SetParent(GameObject.Find("GamePlay").transform, false);
            mCurrentLevelInitialPosition = mCurrentLevel.transform.position;

            CreateNewCurrentGame();
        }
    }

    public void StartGame()
    {
        PlayingLevel = true;
        MenuManager.Instance.DisableCurrentMenu();

        //Set HUD
        mHUDCanvas.SetActive(true);
        StartCountdown();
        mBackgroundScroller.StopScrolling();
    }

    public void StartCountdown()
    {
        
        mCountdownAnimator.enabled = true;
        mCountdownAnimator.Play(0);
    }

    public bool IsCountownEnabled()
    {
        return mCountdownAnimator.enabled;
    }

    public void StartCurrentGame()
    {
        mLeftBoundary.SetActive(false);
        mCurrentGame.StartGame();
        StartLevelScroll();
        mBackgroundScroller.StartScrolling();
    }

    public void PauseGame()
    {
        PauseCurrentGame();
        if (mLevelScroller != null)
        {
            mLevelScroller.StopScrolling();
        }
        mGroundScroller.StopScrolling();
        mBackgroundScroller.StopScrolling();
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming Game");
        ResumeCurrentGame();
        if (mLevelScroller != null)
        {
            mLevelScroller.StartScrolling();
        }
        mGroundScroller.StartScrolling();
        mBackgroundScroller.StartScrolling();
    }

    //TODO:
    public void LevelCompleted()
    {
        mCurrentGame.FinalTime = mCurrentGame.ElapsedTime;
        mCurrentGame.SetStars();
        StopGame();

        //Save Stats
        //TODO: Save player stats

        //load menu
        MenuManager.Instance.PushMenu(MenuManager.Menus.LevelCompleteMenu);
    }

    public void GameOver()
    {
        StopGame();

        //load menu
        MenuManager.Instance.PushMenu(MenuManager.Menus.GameOverLevelMenu);
        mHUDCanvas.SetActive(false);
    }

    public void Respawn()
    {
        CreateNewCurrentGame();
        PlayerMovement.Instance.Respawn();
        mGroundScroller.ResetGround();
        mBackgroundScroller.StartScrolling();

        //Reset Level
        mCurrentLevel.transform.position = mCurrentLevelInitialPosition;
        foreach (Transform child in mCurrentLevel.GetComponentsInChildren<Transform>(true))
        {
            if (!child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(true);
            }
        }
        StartGame();
    }

    public void TutorialRespawnPlayer()
    {
        mCurrentGame.TutorialRespawnPlayer();
    }

    public void ResetEnvironment()
    {
        DestroyCurrentLevel();
        DestroyCurrentGame();
        mCurrentLevel = null;

        PlayerMovement.Instance.Respawn();
        mGroundScroller.ResetGround();
        mBackgroundScroller.StartScrolling();
        Camera.main.transform.position = new Vector3(0.0f, 0.0f, -10.0f);
    }

    public void DestroyCurrentLevel()
    {
        GameObject.Destroy(mCurrentLevel);
    }

    public void StartLevelScroll()
    {
        if (mLevelScroller == null)
        {
            mLevelScroller = mCurrentLevel.AddComponent<LevelScroller>();
        }
        mLevelScroller.Velocity = mGroundVelocity;
    }

    public bool PlayingLevel
    {
        get { return mPlayingLevel; }
        set { mPlayingLevel = value; }
    }

    private void StopGame()
    {
        PlayingLevel = false;
        mLeftBoundary.SetActive(true);

        //Stop Level, Ground, and Background Scroller
        mLevelScroller.StopScrolling();
        mGroundScroller.StopScrolling();
        mBackgroundScroller.StopScrolling();
        mHUDCanvas.SetActive(false);
    }

    #region CurrentGame
    public CurrentGame GetCurrentGame()
    {
        if (mCurrentGame != null)
        {
            return mCurrentGame;
        }
        return null;
    }

    private void CreateNewCurrentGame()
    {
        mCurrentGame = GameObject.Find("GamePlay/Level" + mCurrentLevelId.ToString() + "(Clone)").GetComponent<CurrentGame>();
    }

    private void PauseCurrentGame()
    {
        mCurrentGame.PauseGame();
    }

    private void ResumeCurrentGame()
    {
        mCurrentGame.ResumeGame();
    }

    private void DestroyCurrentGame()
    {
        mCurrentGame = null;
    }

    #endregion

    //Class instance
    private GamePlayManager()
    {
        mGroundScroller = GameObject.Find(mGroundScrollerName).GetComponent<GroundScroller>();
        mBackgroundScroller = Camera.main.GetComponent<BackgroundLines>();
        mHUDCanvas = HelperFunctions.FindInactiveGameObject(mHUDCanvasName);
        mCountdownAnimator = mHUDCanvas.GetComponent<Animator>();
        mCountdownGameObject = mHUDCanvas.transform.Find("Countdown").gameObject;
        mLeftBoundary = GameObject.Find(mLeftBoundaryName);
    }

    private static GamePlayManager instance;

    //CurrentGame
    private CurrentGame mCurrentGame;

    //LevelScroller
    private LevelScroller mLevelScroller;
    private GameObject mLeftBoundary;
    private string mLeftBoundaryName = "LeftBoundary";

    //GroundScroller
    private GroundScroller mGroundScroller;
    private const string mGroundScrollerName = "GroundScroller";
    private Vector3 mGroundVelocity = new Vector3(-3.0f, 0.0f, 0.0f);

    //GroundScroller
    private BackgroundLines mBackgroundScroller;

    //Current Level GameObject
    private GameObject mCurrentLevel;
    private const string prefabPath = "Prefabs/Levels/Level";
    private Vector3 mCurrentLevelInitialPosition;
    private int mCurrentLevelId;

    //HUDCanvas
    private const string mMenuCanvasName = "MenuCanvas";

    //HUDCanvas
    private GameObject mHUDCanvas;
    private const string mHUDCanvasName = "HUDCanvas";
    private Animator mCountdownAnimator;
    private GameObject mCountdownGameObject;

    //Booleans
    private bool mPlayingLevel = false;
}
