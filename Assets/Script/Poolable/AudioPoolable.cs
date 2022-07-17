using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPoolable : PoolableMono
{
    private AudioSource _source = null;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public override void Reset()
    {
        _source.clip = null;
        _source.volume = 1f;
        _source.pitch = 1f;
    }

    /// <summary>
    /// 오디오 재생
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    public void Play(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        _source.Stop();
        _source.clip = clip;
        _source.volume = volume;
        _source.pitch = pitch;
        _source.Play();
        StartCoroutine(WaitForPush(_source.clip.length * 1.05f));
    }

    public void PlayRandomness(AudioClip clip, float randomness = 0.2f, float volume = 1f, float pitch = 1f)
    {
        _source.Stop();
        _source.clip = clip;
        _source.volume = volume;
        _source.pitch = 1f + Random.Range(-randomness, randomness);
        _source.Play();
        StartCoroutine(WaitForPush(_source.clip.length * 1.05f));
    }

    IEnumerator WaitForPush(float time)
    {
        yield return new WaitForSeconds(time);
        PoolManager.Instance.Push(this);
    }
}
