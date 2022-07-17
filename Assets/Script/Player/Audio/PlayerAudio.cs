using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : AudioPlayer
{
    [SerializeField]
    private AudioClip _jumpSound = null;
    [SerializeField]
    private AudioClip _attackSound = null;
    [SerializeField]
    private AudioClip _saveSound = null;
    [SerializeField]
    private AudioClip _dashSound = null;
    /*�����Ҹ�
���ݼҸ�
������ ���̺�
���̺�Ҹ�
��üҸ�
���� Ƚ�� ����*/


    public void PlayJumpSound()
    {
        AudioPoolable audio = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
        audio.PlayRandomness(_jumpSound);
    }
    public void PlayAttackSound()
    {
        AudioPoolable audio = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
        audio.PlayRandomness(_attackSound);
    }
    public void PlaySaveSound()
    {
        AudioPoolable audio = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
        audio.PlayRandomness(_saveSound);
    }
    public void PlayDashSound()
    {
        AudioPoolable audio = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
        audio.PlayRandomness(_dashSound);
    }
}
