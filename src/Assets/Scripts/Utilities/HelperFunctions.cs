using UnityEngine;

public static class HelperFunctions {

    public static GameObject FindInactiveGameObject(string name)
    {
        Transform[] trs = Resources.FindObjectsOfTypeAll<Transform>();
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

    public static string [] ConvertTime(int elapsedTime)
    {
        string[] time = new string[3];
        if (elapsedTime >= 3600)
        {
            time[0] = (elapsedTime / 3600).ToString("00");
            time[1] = ((elapsedTime % 3600) / 60).ToString("00");
            time[2] = ((elapsedTime % 3600) % 60).ToString("00");
        }

        else if (elapsedTime >= 60)
        {
            time[0] = "00";
            time[1] = (elapsedTime / 60).ToString("00");
            time[2] = (elapsedTime % 60).ToString("00");
        }
        else
        {
            time[0] = "00";
            time[1] = "00";
            time[2] = ((elapsedTime % 3600) % 60).ToString("00");
        }

        return time;
    }

}
