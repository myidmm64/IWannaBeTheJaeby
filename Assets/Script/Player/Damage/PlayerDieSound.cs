using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayDieSound()
    {
        AudioPoolable dieClip = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
        dieClip.PlayRandomness(_bloodClip);

        _audioSource.Play();
        _mixer.GetFloat("BGM", out _volume);
        _mixer.SetFloat("BGM", -80f);
    }

    public void StopDieSound()
    {
        _audioSource.Stop();
        _mixer.SetFloat("BGM", _volume);
    }
}
