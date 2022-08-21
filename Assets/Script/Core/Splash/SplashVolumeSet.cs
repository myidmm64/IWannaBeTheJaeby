using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SplashVolumeSet : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _audioMixer = null;
    private float _sfxDb = 0;
    private float _masterDb = 0;
    [SerializeField]
    private float _maxDB = 0f;
    [SerializeField]
    private float _minDB = -40f;

    private void Start()
    {
        _masterDb = PlayerPrefs.GetFloat("MASTER_DB", _maxDB - ((Mathf.Abs(_maxDB) + Mathf.Abs(_minDB)) * 0.01f) * 50f);
        _sfxDb = PlayerPrefs.GetFloat("SFX_DB", _maxDB);

        _audioMixer.SetFloat("Sfx", _sfxDb);
        _audioMixer.SetFloat("Master", _masterDb);
    }
}
