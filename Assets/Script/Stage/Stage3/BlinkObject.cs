using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlinkObject : MonoBehaviour
{
    [SerializeField]
    private float _fadeInDuration = 2f;
    [SerializeField]
    private float _fadeOutDuration = 2f;
    [SerializeField]
    private Color _startColor = Color.white;

    private SpriteRenderer _spriteRenderer = null;
    private Sequence _seq = null;

    private void OnEnable()
    {
        if(_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        if(_seq != null)
            _seq.Kill();
        _spriteRenderer.color = _startColor;
        _seq = DOTween.Sequence();
        _seq.Append(_spriteRenderer.DOFade(0.6f, _fadeInDuration).SetEase(Ease.Linear));
        _seq.Append(_spriteRenderer.DOFade(0f, _fadeOutDuration).SetEase(Ease.Linear)).SetLoops(-1);
    }

}
