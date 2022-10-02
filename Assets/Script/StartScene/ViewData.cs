using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewData : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _mapText = null;
    [SerializeField]
    private TextMeshProUGUI _diffText = null;
    [SerializeField]
    private TextMeshProUGUI _achieveText = null;
    [SerializeField]
    private TextMeshProUGUI _playTimeText = null;
    [SerializeField]
    private TextMeshProUGUI _deathText = null;


    public void View()
    {
        if (PlayerPrefs.GetString("SAVE_DIFFICULTY") == "None" || PlayerPrefs.GetString("SAVE_DIFFICULTY") == "")
        {
            TextReset();
            return;
        }

        _mapText.SetText($"맵 - {PlayerPrefs.GetString("SAVE_MAP", $"1Map_0")}");
        _diffText.SetText($"난이도 - {PlayerPrefs.GetString("SAVE_DIFFICULTY", "Normal")}");
        _achieveText.SetText($"업적 - {PlayerPrefs.GetInt("SAVE_ACHIEVEMENT", 0)}/6");
        string playTime = PlayerPrefs.GetFloat("SAVE_PLAYTIME", 0f).ToString("N2");
        _playTimeText.SetText($"플레이타임 - {playTime}");
        _deathText.SetText($"데스 - {PlayerPrefs.GetInt("SAVE_DEATHCOUNT", 0)}");
    }

    public void Exit()
    {
        TextReset();
    }

    private void TextReset()
    {
        _mapText.SetText("");
        _diffText.SetText("");
        _achieveText.SetText("");
        _playTimeText.SetText("");
        _deathText.SetText("");
    }
}
