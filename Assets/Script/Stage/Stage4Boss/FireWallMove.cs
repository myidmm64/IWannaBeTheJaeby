using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class FireWallMove : MonoBehaviour
{
    private Sequence _seq = null;
    private Vector3 _originPos = Vector3.zero;
    [field: SerializeField]
    private UnityEvent OnPatternEnd = null;
    [SerializeField]
    private bool _isRight = true;
    private void OnEnable()
    {
        if (_originPos == Vector3.zero)
        {
            _originPos = transform.position;
        }
        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();
        if(_isRight)
        {
            _seq.Append(transform.DOMoveX(2.8f, 5f).SetEase(Ease.Linear));
            _seq.Append(transform.DOMoveX(_originPos.x, 1f));
        }
        else
        {
            _seq.Append(transform.DOMoveX(-2.8f, 5f).SetEase(Ease.Linear));
            _seq.Append(transform.DOMoveX(_originPos.x, 1f));
        }
        _seq.AppendCallback(() =>
        {
            OnPatternEnd?.Invoke();
        });
    }

    private void OnDisable()
    {
        if (_seq != null)
            _seq.Kill();

        if (_originPos != Vector3.zero)
        {
            transform.position = _originPos;
        }
    }
}
