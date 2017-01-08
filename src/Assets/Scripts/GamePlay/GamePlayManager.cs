using UnityEngine;

public class GamePlayManager {

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

    public void LoadLevel(int id)
    {
        if (!PlayingLevel)
        {
            mCurrentLevel = GameObject.Instantiate(Resources.Load(prefabPath + id.ToString(), typeof(GameObject))) as GameObject;
            mCurrentLevel.transform.SetParent(GameObject.Find("GamePlay").transform, false);
            mCurrentLevelInitialPosition = mCurrentLevel.transform.position;
        }
    }

    public void StartGame()
    {
        mPlayingLevel = true;
        MenuManager.Instance.DisableCurrentMenu();

        //Set HUD
        mHUDCanvas.SetActive(true);
    }

    //TODO:
    public void LevelCompleted()
    {
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

    public void ResetEnvironment()
    {
        DestroyCurrentLevel();
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

    public void StartLevelScroll(Vector3 velocity)
    {
        mLevelScroller = mCurrentLevel.AddComponent<LevelScroller>();
        mLevelScroller.Velocity = velocity;
    }

    public bool PlayingLevel
    {
        get { return mPlayingLevel; }
        set { mPlayingLevel = value; }
    }

    private void StopGame()
    {
        mPlayingLevel = false;

        //Stop Level, Ground, and Background Scroller
        mLevelScroller.StopScrolling();
        mGroundScroller.StopScrolling();
        mBackgroundScroller.StopScrolling();
        mHUDCanvas.SetActive(false);
    }

    //Class instance
    private GamePlayManager()
    {
        mGroundScroller = GameObject.Find(mGroundScrollerName).GetComponent<GroundScroller>();
        mBackgroundScroller = Camera.main.GetComponent<BackgroundLines>();
        mHUDCanvas = HelperFunctions.FindInactiveGameObject(mHUDCanvasName);
        mHUDManager = mHUDCanvas.GetComponent<HUDManager>();
        mMenuCanvas = GameObject.Find(mMenuCanvasName);
    }

    
    private static GamePlayManager instance;

    //LevelScroller
    private LevelScroller mLevelScroller;

    //GroundScroller
    private GroundScroller mGroundScroller;
    private const string mGroundScrollerName = "GroundScroller";

    //GroundScroller
    private BackgroundLines mBackgroundScroller;

    //Current Level GameObject
    private GameObject mCurrentLevel;
    private const string prefabPath = "Prefabs/Levels/Level";
    private Vector3 mCurrentLevelInitialPosition;

    //HUDCanvas
    private GameObject mMenuCanvas;
    private const string mMenuCanvasName = "MenuCanvas";

    //HUDCanvas
    private GameObject mHUDCanvas;
    private HUDManager mHUDManager;
    private const string mHUDCanvasName = "HUDCanvas";

    //Booleans
    private bool mPlayingLevel = false;
}
