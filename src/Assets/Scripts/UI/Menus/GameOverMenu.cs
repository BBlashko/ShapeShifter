public class GameOverMenu : BaseMenu {

    public static GameOverMenu Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameOverMenu();
            }
            return instance;
        }
    }

    public void RespawnButton()
    {
        GamePlayManager.Instance.Respawn();
    }

    public void LevelSelectButton()
    {
        GamePlayManager.Instance.ResetEnvironment();
        DoBack();
    }

    private GameOverMenu() { }
    private static GameOverMenu instance;
}
