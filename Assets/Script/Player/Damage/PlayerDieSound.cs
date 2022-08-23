using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public class PlayerDieSound : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _mixer;
    [SerializeField]
    private AudioClip _bloodClip = null;
    private AudioSource _audioSource;
    private float _volume;

    [SerializeField]
    private TextMeshProUGUI[] _text;
    private Vector3[] _originPos = new Vector3[2];

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _originPos[0] = _text[0].transform.position;
        _originPos[1] = _text[1].transform.position;
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

        _text[0].transform.DOShakePosition(0.1f, 10f).SetLoops(-1, LoopType.Yoyo);

        _text[1].transform.DOShakePosition(0.1f, 7f, 5).SetLoops(-1, LoopType.Yoyo);
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

        _text[0].transform.DOKill();
        _text[1].transform.DOKill();
        _text[0].transform.SetPositionAndRotation(_originPos[0], Quaternion.identity);
        _text[1].transform.SetPositionAndRotation(_originPos[1], Quaternion.identity);
    }
}
