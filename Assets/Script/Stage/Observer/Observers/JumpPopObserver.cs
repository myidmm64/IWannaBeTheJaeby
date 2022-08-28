using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpPopObserver : MonoBehaviour, IObserver
{
    [SerializeField]
    private bool _firstPop = false;
    private bool _poped = false;
    [SerializeField]
    private float _popAmount = 0.5f;
    [SerializeField]
    private float _popSpeed = 0.2f;
    private Vector3 _originPos = Vector3.zero;
    private bool _isFirst = true;
    Sequence _seq = null;

    private void OnEnable()
    {
        _poped = _firstPop;

        if (_isFirst)
        {
            _isFirst = false;

            if(_firstPop)
            {
                _originPos = transform.position - (transform.up * _popAmount);
            }
            else
                _originPos = transform.position;
        }
    }

    public void Observed()
    {
        if (_seq != null)
        {
            _seq.Kill();
        }
        _seq = DOTween.Sequence();

        if (_poped)
            _seq.Append(transform.DOMove(_originPos, _popSpeed));
        else
            _seq.Append(transform.DOMove(_originPos + transform.up * _popAmount, _popSpeed));
        _poped = !_poped;
    }

    private void OnDisable()
    {
        if (_seq != null)
            _seq.Kill();
        transform.DOKill();
        _poped = _firstPop;

        if(_firstPop)
            transform.position = _originPos + (transform.up * _popAmount);
        else
            transform.position = _originPos;
    }
}
