using UnityEngine;
using System.Collections;
using System;

public class MenuManager {

    public enum Menus { MainMenu = 0,
                        PlayMenu = 1,
                        LevelSelectMenu = 2,
                        DifficultyMenu = 3,
                        ExitMenu = 4 }

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

    public void PushMenu(Menus menu)
    {
        if (mCurrentMenu != null)
        {
            mCurrentMenu.SetActive(false);

            //never add the ExitMenu to the stack
            if (mCurrentMenu.name != "ExitMenu")
            {
                mMenuStack.Push(mCurrentMenu);
            }
        }
        mCurrentMenu = mMenuGameObjects[Convert.ToInt32(menu)];
        mCurrentMenu.SetActive(true);
       
    }

    public void PopMenu()
    {
        if (mMenuStack.Count != 0)
        {
            mCurrentMenu.SetActive(false);
            mCurrentMenu = (GameObject) mMenuStack.Pop();
            mCurrentMenu.SetActive(true);
        }
        else
        {
            //no menus left to pop. Display ExitMenu
            PushMenu(Menus.ExitMenu);
        }
    }

    //Load all different menus in the scene
    private void LoadMenus()
    {
        string[] names = Enum.GetNames(typeof(Menus));
        mMenuGameObjects = new GameObject[names.Length];
        mParentMenuGameObject = GameObject.Find("MenuCanvas");

        for (int i = 0; i < names.Length; i++)
        {
            Transform childTransform = mParentMenuGameObject.transform.FindChild(names[i]);
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
}
