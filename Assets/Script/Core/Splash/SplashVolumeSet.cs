using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SplashVolumeSet : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _audioMixer = null;
    private float _sfxDb = 0;
    private float _bGMDb = 0;
    private float _masterDb = 0;
    [SerializeField]
    private float _maxDB = 0f;
    [SerializeField]
    private float _minDB = -40f;

    private void Start()
    {
        _masterDb = PlayerPrefs.GetFloat("MASTER_DB", _maxDB - ((Mathf.Abs(_maxDB) + Mathf.Abs(_minDB)) * 0.01f) * 50f);
        _bGMDb = PlayerPrefs.GetFloat("BGM_DB", _maxDB);
        _sfxDb = PlayerPrefs.GetFloat("SFX_DB", _maxDB);

        if (_masterDb == _minDB)
            _audioMixer.SetFloat("Master", -80f);
        if (_bGMDb == _minDB)
            _audioMixer.SetFloat("BGM", -80f);
        if (_sfxDb == _minDB)
        {
            _audioMixer.SetFloat("Sfx", -80f);
            _audioMixer.SetFloat("Rain", -80f);
            _audioMixer.SetFloat("Death", -80f);
        }

        _audioMixer.SetFloat("Master", _masterDb);
        _audioMixer.SetFloat("Sfx", _sfxDb);
        _audioMixer.SetFloat("Rain", _sfxDb + 5f);
        _audioMixer.SetFloat("BGM", _bGMDb);
        _audioMixer.SetFloat("Death", _sfxDb);
    }
}
