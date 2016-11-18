using UnityEngine;

public class PlayMenu : BaseMenu {

    public static PlayMenu Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayMenu();
            }
            return instance;
        }
    }

    public void CampaignButton()
    {
        ChangeMenu(MenuManager.Menus.LevelSelectMenu);
    }

    public void InfinityButton()
    {
        ChangeMenu(MenuManager.Menus.DifficultyMenu);
    }

    private PlayMenu() { }
    private static PlayMenu instance;
}
