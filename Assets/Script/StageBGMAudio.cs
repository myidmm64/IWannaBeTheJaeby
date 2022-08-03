using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBGMAudio : AudioPlayer
{
    [SerializeField]
    private AudioClip _normalBGM = null;
    public AudioClip NormalBGM
    {
        get => _normalBGM;
        set => _normalBGM = value;
    }

    [SerializeField]
    private AudioClip _BossBGM = null;

    public void NormalBGMPlay()
    {
        PlayClip(_normalBGM);
    }
    public void BossBGMPlay()
    {
        PlayClip(_BossBGM);
    }
    public void BGMPlay(AudioClip clip)
    {
        PlayClip(clip);
    }
}
