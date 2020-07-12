using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    //Master keys
    const string VIBRATION_KEY = "Vibrate Toggle";

    const int VIBRATETOGGLE = 1;
    const int SOUNDTOGGLE = 1;

    public static void SetVibrate(bool toggle)
    {
        int value = toggle == true ? 1 : 0;
        PlayerPrefs.SetInt(VIBRATION_KEY, value);
    }

    public static int GetVibration()
    {
        return PlayerPrefs.GetInt(VIBRATION_KEY);
    }

}
