using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ResolutionManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _resolutionText = null;
    [SerializeField]
    private TextMeshProUGUI _fullScreenText = null;
    [field: SerializeField]
    private UnityEvent OnWarring = null;

    List<Resolution> resolutions = new List<Resolution>();
    private int _resolNum = 0;

    private bool _fullScreen = false;

    private void InitUI()
    {
        List<Resolution> resolutionTemp = new List<Resolution>();
        resolutionTemp.AddRange(Screen.resolutions);

        resolutions = resolutionTemp.FindAll(x => x.refreshRate == 60 || x.refreshRate == 144 || x.refreshRate > 144);

        foreach (var item in resolutions)
        {
            Debug.Log(item.width + "X" + item.height + " " + item.refreshRate);
        }

        _resolNum = PlayerPrefs.GetInt("RESOLUTION_NUMBER", resolutions.Count - 1);
        _fullScreen = PlayerPrefs.GetInt("FULL_SCREEN", 1) == 0 ? false : true;

        Screen.SetResolution(resolutions[_resolNum].width, resolutions[_resolNum].height, _fullScreen);
        _resolutionText?.SetText("< " + resolutions[_resolNum].width + "x" + resolutions[_resolNum].height + " " + resolutions[_resolNum].refreshRate + " >");
        _fullScreenText?.SetText(_fullScreen == false ? "< 창화면 >" : "< 전체화면 >");
    }

    public void ChangeResolutionUp()
    {
        _resolNum++;
        _resolNum = Mathf.Clamp(_resolNum, 0, resolutions.Count - 1);
        Screen.SetResolution(resolutions[_resolNum].width, resolutions[_resolNum].height, _fullScreen);

        _resolutionText?.SetText("< " + resolutions[_resolNum].width + "x" + resolutions[_resolNum].height + " " + resolutions[_resolNum].refreshRate + " >");
        PlayerPrefs.SetInt("RESOLUTION_NUMBER",_resolNum);
        OnWarring?.Invoke();
    }
    public void ChangeResolutionDown()
    {
        _resolNum--;
        _resolNum = Mathf.Clamp(_resolNum, 0, resolutions.Count - 1);
        Screen.SetResolution(resolutions[_resolNum].width, resolutions[_resolNum].height, _fullScreen);

        _resolutionText?.SetText("< " + resolutions[_resolNum].width + "x" + resolutions[_resolNum].height + " " + resolutions[_resolNum].refreshRate + " >");
        PlayerPrefs.SetInt("RESOLUTION_NUMBER", _resolNum);
        OnWarring?.Invoke();
    }
    public void SetFullScreen()
    {
        _fullScreen = !_fullScreen;
        Screen.SetResolution(resolutions[_resolNum].width, resolutions[_resolNum].height, _fullScreen);

        _fullScreenText?.SetText(_fullScreen == false ? "< 창화면 >" : "< 전체화면 >");
        PlayerPrefs.SetInt("FULL_SCREEN", _fullScreen == false ? 0 : 1);
    }

    private void Start()
    {
        InitUI();
    }
}
