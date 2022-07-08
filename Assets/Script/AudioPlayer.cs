using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    protected AudioSource _audioSorce;
    [SerializeField]
    protected float _pitchRandomness = 0.2f;
    protected float _basePitch = 0f;
    private void Awake()
    {
        _audioSorce = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _basePitch = _audioSorce.pitch;
    }
    //Ŭ���� ������ġ��
    protected void PlayClipWithVariablePitch(AudioClip clip)
    {
        float randomPitch = Random.Range(-_pitchRandomness, _pitchRandomness);
        _audioSorce.pitch = _basePitch + randomPitch;
        PlayClip(clip);
    }
    //��ġ �������� �׳� ���
    protected void PlayClip(AudioClip clip)
    {
        _audioSorce.Stop();
        _audioSorce.clip = clip;
        _audioSorce.Play();
    }
}
