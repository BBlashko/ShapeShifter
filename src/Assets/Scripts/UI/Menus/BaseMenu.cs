public class BaseMenu {

    public void ChangeMenu(MenuManager.Menus Menu)
    {
        MenuManager.Instance.PushMenu(Menu);
    }

    //For android back button. Or whenever user
    //wants to go to previous screen.
    public void DoBack()
    {
        MenuManager.Instance.PopMenu();
    }
}
