using UnityEngine;

public class ExitMenu : BaseMenu
{

    public static ExitMenu Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ExitMenu();
            }
            return instance;
        }
    }

    public void YesButton()
    {
#if UNITY_IPHONE || UNITY_ANDROID
        Application.Quit();
#else
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void NoButton()
    {
        DoBack();
    }

    private ExitMenu() { }
    private static ExitMenu instance;
}
