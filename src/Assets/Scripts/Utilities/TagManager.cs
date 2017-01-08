using UnityEngine;

public class TagManager {

    public const string Background = "Background";
    public const string Ground = "Ground";
    public const string PlayerShape = "PlayerShape";
    public const string Boundary = "Boundary";
    public const string LeftBoundary = "LeftBoundary";
    public const string Token = "Token";
    public const string Obstacle = "Obstacle";
    public const string Triangle = "Triangle";
    public const string Rectangle = "Rectangle";
    public const string Square = "Square";
    public const string LevelComplete = "LevelComplete";

    public static TagManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TagManager();
            }
            return instance;
        }
    }

    //Class instance
    private TagManager() { }
    private static TagManager instance;
}
