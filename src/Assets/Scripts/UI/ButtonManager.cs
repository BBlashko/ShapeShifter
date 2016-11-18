using UnityEngine;

/* Class contains all button clicks in the menu */

public class ButtonManager : MonoBehaviour {

    void Start()
    {
        MenuManager.Instance.PushMenu(MenuManager.Menus.MainMenu);
    }

    #region MainMenu
    public void PlayButton()
    {
        MainMenu.Instance.PlayButton();
    }

    public void StatisticsButton()
    {
        MainMenu.Instance.StatisticsButton();
    }

    public void OptionsButton()
    {
        MainMenu.Instance.OptionsButton();
    }

    public void ExitButton()
    {
        MainMenu.Instance.ExitButton();
    }
    #endregion

    #region PlayMenu
    public void CampaignButton()
    {
        PlayMenu.Instance.CampaignButton();
    }

    public void InfinityButton()
    {
        PlayMenu.Instance.InfinityButton();
    }

    #endregion

    #region DifficultyMenu
    public void EasyButton()
    {
        DifficultyMenu.Instance.EasyButton();
    }

    public void MediumButton()
    {
        DifficultyMenu.Instance.MediumButton();
    }

    public void HardButton()
    {
        DifficultyMenu.Instance.HardButton();
    }

    public void VeryHardButton()
    {
        DifficultyMenu.Instance.VeryHardButton();
    }

    #endregion

    #region LevelSelectMenu
    public void LevelButton(int i)
    {
        LevelSelectMenu.Instance.LevelButton(i);
    }
    #endregion

    #region UtilButtons
    public void BackButton()
    {
        MenuManager.Instance.PopMenu();
    }
    #endregion

    #region ExitMenu
    public void YesButton()
    {
        ExitMenu.Instance.YesButton();
    }

    public void NoButton()
    {
        ExitMenu.Instance.NoButton();
    }
    #endregion



    //menu objects
    private MainMenu mMainMenu;
    private PlayMenu mPlayMenu;
}
