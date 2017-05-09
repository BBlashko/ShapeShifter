public class LevelSelectMenu : BaseMenu
{

    public static LevelSelectMenu Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LevelSelectMenu();
            }
            return instance;
        }
    }

    public void LevelButton(int i)
    {
        if (i == 1)
        {
            //level is tutorial level
            GamePlayManager.Instance.LoadLevel(0);
        }
        else
        {
            //level is not a tutorial level
            GamePlayManager.Instance.LoadLevel(i);
            GamePlayManager.Instance.StartGame();
        }
    }

    private LevelSelectMenu() { }
    private static LevelSelectMenu instance;
}
