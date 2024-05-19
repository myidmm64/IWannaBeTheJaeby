using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    List<Resolution> resolutions = new List<Resolution>();
    private int _resolNum = 0;

    private bool _fullScreen = false;

    void Start()
    {
        List<Resolution> resolutionTemp = new List<Resolution>();
        resolutionTemp.AddRange(Screen.resolutions);
        resolutions = resolutionTemp.FindAll(x => x.refreshRate == 60 || x.refreshRate == 144 || x.refreshRate > 144);

        if (resolutions.Count > 0 )
        {
            _resolNum = PlayerPrefs.GetInt("RESOLUTION_NUMBER", resolutions.Count - 1);
            _fullScreen = PlayerPrefs.GetInt("FULL_SCREEN", 1) == 0 ? false : true;

            Screen.SetResolution(resolutions[_resolNum].width, resolutions[_resolNum].height, _fullScreen);
        }
    }
}
