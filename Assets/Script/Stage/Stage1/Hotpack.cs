using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Hotpack : MonoBehaviour
{
    [SerializeField]
    private float _duration = 1f;
    [SerializeField]
    private float _jumpPower = 20f;
    [SerializeField]
    private AudioClip _boomClip = null;

    private Sequence _seq = null;
    private AgentJump _agentJump = null;
    private SpriteRenderer _spriteRenderer = null;

    private void OnEnable()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();
        HotPackReset();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _agentJump ??= collision.transform.parent.GetComponent<AgentJump>();
            HotPackStart();
        }
    }

    private void HotPackStart()
    {
        if (_agentJump == null) return;
        _seq = DOTween.Sequence();
        _seq.Append(_spriteRenderer.DOColor(Color.red, _duration));
        _seq.AppendCallback(() =>
        {
            AudioPoolable a = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
            a.Play(_boomClip);
            _agentJump.ForceJump(_jumpPower, Vector3.up);
        });
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HotPackReset();
        }
    }

    private void HotPackReset()
    {
        if (_seq != null)
            _seq.Kill();
        _spriteRenderer.color = Color.white;
        _agentJump = null;
    }
}
