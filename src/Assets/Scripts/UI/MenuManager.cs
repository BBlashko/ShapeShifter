using UnityEngine;
using System.Collections;
using System;

public class MenuManager {

    public enum Menus { MainMenu = 0,
        PlayMenu = 1,
        LevelSelectMenu = 2,
        DifficultyMenu = 3,
        ExitMenu = 4,
        GameOverLevelMenu = 5,
        LevelCompleteMenu = 6 }

    public static MenuManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MenuManager();
            }
            return instance;
        }
    }

    public void DisableCurrentMenu()
    {
        if (mCurrentMenu != null)
        {
            mCurrentMenu.SetActive(false);  
        }
    }

    public void EnableCurrentMenu()
    {
        if (mCurrentMenu != null)
        {
            mCurrentMenu.SetActive(true);
        }
    }

    ////Display menu without adding to the menu stack
    ////Thus, this menu can not be returned to if pop attempted.
    //public void DisplayMenu(Menus menu)
    //{
    //    if (mCurrentMenu != null)
    //    {
    //        mCurrentMenu.SetActive(false);
    //    }
    //    mCurrentMenu = mMenuGameObjects[Convert.ToInt32(menu)];
    //    mCurrentMenu.SetActive(true);
    //}

    //Display menu and add to the stack to be popped by a back button
    public void PushMenu(Menus menu)
    {
        if (mCurrentMenu != null)
        {
            mCurrentMenu.SetActive(false);

            //never add the ExitMenu or GameOverLevelMenu to the stack
            if (CheckIfStackableMenu(menu))
            {
                mMenuStack.Push(mCurrentMenu);
            }
        }
        mCurrentMenu = mMenuGameObjects[Convert.ToInt32(menu)];
        mCurrentMenu.SetActive(true);

    }

    public void PopMenu()
    {
        if (mMenuStack.Count != 0 && !GamePlayManager.Instance.PlayingLevel)
        {
            mCurrentMenu.SetActive(false);
            mCurrentMenu = (GameObject)mMenuStack.Pop();
            mCurrentMenu.SetActive(true);
        }
        else
        {
            //no menus left to pop. Display ExitMenu
            PushMenu(Menus.ExitMenu);
        }
    }

    private bool CheckIfStackableMenu(Menus menu)
    {
        foreach (string name in mNonStackableMenuNames)
        {
            if (mCurrentMenu.name == name)
            {
                return false;
            }
        }
        return true;
    }

    //Load all different menus in the scene
    private void LoadMenus()
    {
        string[] names = Enum.GetNames(typeof(Menus));
        mMenuGameObjects = new GameObject[names.Length];
        mParentMenuGameObject = GameObject.Find("MenuCanvas");

        for (int i = 0; i < names.Length; i++)
        {
            Transform childTransform = mParentMenuGameObject.transform.Find(names[i]);
            mMenuGameObjects[i] = childTransform.gameObject;
        }
    }

    private MenuManager()
    {
        LoadMenus();
        mMenuStack = new Stack();
    }

    //Instance
    private static MenuManager instance;
    private Stack mMenuStack;

    //GameObjects
    private GameObject mCurrentMenu;
    private GameObject mParentMenuGameObject;
    private GameObject[] mMenuGameObjects;

    //Non stackable menus
    private string[] mNonStackableMenuNames = new string[] { "ExitMenu",
                                                             "GameOverLevelMenu",
                                                             "LevelCompleteMenu"
                                                           };
}
