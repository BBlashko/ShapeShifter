using UnityEngine;
using System.Collections;

public class TokenManager : Token {

    public static TokenManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TokenManager();
            }
            return instance;
        }
    }

    private TokenManager() { }
    private static TokenManager instance;
}
