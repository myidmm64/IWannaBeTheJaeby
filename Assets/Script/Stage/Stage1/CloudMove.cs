using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloudMove : MonoBehaviour
{
    [SerializeField, Header("사라질 시간")]
    private float _fadeTime = 2f;
    [SerializeField, Header("재생성 시간")]
    private float _interval = 1f;
    [SerializeField]
    private float _reSummonTime = 2f;
    private Collider2D _col = null;
    private SpriteRenderer _spriteRenderer = null;
    private Sequence _seq = null;

    private void OnEnable()
    {
        if(_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if(_col == null)
        {
            _col = GetComponent<Collider2D>();
        }

        if (_seq != null)
            _seq.Kill();
        _spriteRenderer.color = Color.white;
        _col.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Fade();
        }
    }

    private void Fade()
    {
        _seq = DOTween.Sequence();
        _seq.Append(_spriteRenderer.DOFade(0.1f, _fadeTime).SetEase(Ease.Linear));
        _seq.AppendCallback(() =>
        {
            _col.enabled = false;
            _spriteRenderer.DOFade(0f, 0f);
        });
        _seq.AppendInterval(_interval);
        _seq.Append(_spriteRenderer.DOFade(0.8f, _reSummonTime).SetEase(Ease.Linear));
        _seq.AppendCallback(() =>
        {
            _col.enabled = true;
        });
    }


}
