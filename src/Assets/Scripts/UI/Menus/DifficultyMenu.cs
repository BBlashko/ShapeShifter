public class DifficultyMenu : BaseMenu {

    public static DifficultyMenu Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DifficultyMenu();
            }
            return instance;
        }
    }

    public void EasyButton()
    {
        //TODO
    }

    public void MediumButton()
    {
        //TODO
    }

    public void HardButton()
    {
        //TODO
    }

    public void VeryHardButton()
    {
        //TODO
    }

    private DifficultyMenu() { }
    private static DifficultyMenu instance;
}
