using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerDieSound : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _mixer;
    [SerializeField]
    private AudioClip _bloodClip = null;
    private AudioSource _audioSource;
    private float _volume;

    private TextMeshPro[] _text = new TextMeshPro[2];

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _text[0] = GetComponent<TextMeshPro>();
        _text[1] = transform.Find("Text2").GetComponent<TextMeshPro>();
    }

    public void PlayDieSound()
    {
        AudioPoolable dieClip = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
        dieClip.PlayRandomness(_bloodClip);

        _audioSource.Play();
        _mixer.GetFloat("BGM", out _volume);
        _mixer.SetFloat("BGM", -80f);

        _text[0].enabled = true;
        _text[1].enabled = true;
    }

    public void StopDieSound()
    {
        _audioSource.Stop();
        _mixer.SetFloat("BGM", _volume);

        if(_volume == -80f)
        {
            _mixer.SetFloat("BGM", 0f);
        }

        _text[0].enabled = false;
        _text[1].enabled = false;
    }
}
