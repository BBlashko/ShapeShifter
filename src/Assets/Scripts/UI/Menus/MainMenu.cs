public class MainMenu : BaseMenu {

    public static MainMenu Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MainMenu();
            }
            return instance;
        }
    }

    public void PlayButton()
    {
        ChangeMenu(MenuManager.Menus.PlayMenu);
    }

    public void StatisticsButton()
    {
        //TODO:
        // ChangeMenu(MenuManager.Menus.StatisticsMenu);
    }

    public void OptionsButton()
    {
        //TODO:
        // ChangeMenu(MenuManager.Menus.OptionsMenu);
    }

    public void ExitButton()
    {
        ChangeMenu(MenuManager.Menus.ExitMenu);
    }

    private MainMenu() { }
    private static MainMenu instance;

}
