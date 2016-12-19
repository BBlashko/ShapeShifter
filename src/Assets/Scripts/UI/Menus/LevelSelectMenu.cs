public class LevelSelectMenu : BaseMenu {

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
        GamePlayManager.Instance.LoadLevel(i);
        GamePlayManager.Instance.StartGame();
    }

    private LevelSelectMenu() { }
    private static LevelSelectMenu instance;
}
