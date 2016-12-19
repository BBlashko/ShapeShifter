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
        mCurrentLevel = GameObject.Instantiate(Resources.Load(prefabPath + id.ToString(), typeof(GameObject))) as GameObject;
        mCurrentLevel.transform.SetParent(GameObject.Find("GamePlay").transform, false);
    }

    public void StartGame()
    {
        mPlayingLevel = true;
        mMenuCanvas.SetActive(false);
        mHUDCanvas.SetActive(true);
    }

    public void StartLevelScroll(Vector3 velocity)
    {
        LevelScroller levelScroller = mCurrentLevel.AddComponent<LevelScroller>();
        levelScroller.Velocity = velocity;
    }

    public bool PlayingLevel
    {
        get { return mPlayingLevel; }
        set { mPlayingLevel = value; }
    }


    //Class instance
    private GamePlayManager()
    {
        mGroundScroller = GameObject.Find(mGroundScrollerName).GetComponent<GroundScroller>();
        mHUDCanvas = HelperFunctions.FindInActiveGameObject(mHUDCanvasName);
        mMenuCanvas = GameObject.Find(mMenuCanvasName);
    }

    
    private static GamePlayManager instance;

    //GroundScroller
    private GroundScroller mGroundScroller;
    private const string mGroundScrollerName = "GroundScroller";

    //Current Level GameObject
    private GameObject mCurrentLevel;
    private const string prefabPath = "Prefabs/Levels/Level";

    //HUDCanvas
    private GameObject mMenuCanvas;
    private const string mMenuCanvasName = "MenuCanvas";

    //HUDCanvas
    private GameObject mHUDCanvas;
    private const string mHUDCanvasName = "HUDCanvas";

    //Booleans
    private bool mPlayingLevel = false;
}
