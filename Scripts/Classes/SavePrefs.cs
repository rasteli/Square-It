using UnityEngine;

public static class SavePrefs
{
    public static void SaveState(string data, float index)
    {
        PlayerPrefs.SetFloat(data, index);
    }

    public static float LoadState(string data, float defaultIndex = 0)
    {
        return PlayerPrefs.GetFloat(data, defaultIndex);
    }
}
