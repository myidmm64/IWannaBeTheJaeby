using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _audioMixer = null;
    private float _masterDb = 0;
    private float _bGMDb = 0;
    private float _sfxDb = 0;
    [SerializeField]
    private float _maxDB = 0f;
    [SerializeField]
    private float _minDB = -40f;
    [SerializeField]
    private TextMeshProUGUI _masterText = null;
    [SerializeField]
    private TextMeshProUGUI _bGMText = null;
    [SerializeField]
    private TextMeshProUGUI _sfxText = null;


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _masterDb = PlayerPrefs.GetFloat("MASTER_DB", _maxDB);
        _bGMDb = PlayerPrefs.GetFloat("BGM_DB", _maxDB);
        _sfxDb = PlayerPrefs.GetFloat("SFX_DB", _maxDB);

        SetMixer();
    }

    public void MasterDbDown()
    {
        Debug.Log(_masterDb);
        _masterDb -= ((Mathf.Abs(_maxDB) + Mathf.Abs(_minDB)) * 0.01f) * 10f;
        Debug.Log(_masterDb);
        _masterDb = Mathf.Clamp(_masterDb, _minDB, _maxDB);
        SetMixer();
    }
    public void MasterDbUp()
    {
        _masterDb += ((Mathf.Abs(_maxDB) + Mathf.Abs(_minDB)) * 0.01f) * 10f;
        _masterDb = Mathf.Clamp(_masterDb, _minDB, _maxDB);
        SetMixer();
    }

    public void BGMDbUp()
    {
        _bGMDb += ((Mathf.Abs(_maxDB) + Mathf.Abs(_minDB)) * 0.01f) * 10f;
        _bGMDb = Mathf.Clamp(_bGMDb, _minDB, _maxDB);
        SetMixer();
    }
    public void BGMDbDown()
    {
        _bGMDb -= ((Mathf.Abs(_maxDB) + Mathf.Abs(_minDB)) * 0.01f) * 10f;
        _bGMDb = Mathf.Clamp(_bGMDb, _minDB, _maxDB);
        SetMixer();
    }

    public void SfxDbUp()
    {
        _sfxDb += ((Mathf.Abs(_maxDB) + Mathf.Abs(_minDB)) * 0.01f) * 10f;
        _sfxDb = Mathf.Clamp(_sfxDb, _minDB, _maxDB);
        SetMixer();
    }
    public void SfxDbDown()
    {
        _sfxDb -= ((Mathf.Abs(_maxDB) + Mathf.Abs(_minDB)) * 0.01f) * 10f;
        _sfxDb = Mathf.Clamp(_sfxDb, _minDB, _maxDB);
        SetMixer();
    }

    private void SetMixer()
    {
        _audioMixer.SetFloat("Master", _masterDb);
        _audioMixer.SetFloat("Sfx", _sfxDb);
        _audioMixer.SetFloat("Rain", _sfxDb + 5f);
        _audioMixer.SetFloat("BGM", _bGMDb);
        _audioMixer.SetFloat("Death", _sfxDb);

        PlayerPrefs.SetFloat("MASTER_DB", _masterDb);
        PlayerPrefs.SetFloat("BGM_DB", _bGMDb);
        PlayerPrefs.SetFloat("SFX_DB", _sfxDb);

        float max = ((Mathf.Abs(_maxDB) + Mathf.Abs(_minDB)) * 0.01f) * 100f;
        float time = 100 / max;
        float masterResult = _masterDb * time + 100;
        float bGMResult = _bGMDb * time + 100;
        float sfxResult = _sfxDb * time + 100;


        string masterText = masterResult.ToString("N0");
        _masterText.SetText("< 전체 " + masterText + "% >");

        string bGMText = bGMResult.ToString("N0");
        _bGMText.SetText("< 배경음 " + bGMText + "% >");

        string sfxText = sfxResult.ToString("N0");
        _sfxText.SetText("< 효과음 " + sfxText + "% >");
    }
}

