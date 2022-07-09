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
        PlayClipWithVariablePitch(_jumpSound);
    }
    public void PlayAttackSound()
    {
        PlayClipWithVariablePitch(_attackSound);
    }
    public void PlaySaveSound()
    {
        PlayClipWithVariablePitch(_saveSound);
    }
    public void PlayDashSound()
    {
        PlayClipWithVariablePitch(_dashSound);
    }
}
