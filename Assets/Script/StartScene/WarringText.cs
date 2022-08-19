using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WarringText : MonoBehaviour
{
    [SerializeField]
    private Transform _targetPos = null;
    private Sequence _seq = null;
    private Vector3 _originPos = Vector3.zero;

    private void Awake()
    {
        _originPos = transform.position;
    }

    public void Warring()
    {
        if (_seq != null)
            _seq.Kill();
        transform.position = _originPos;

        _seq = DOTween.Sequence();
        _seq.Append(transform.DOMove(_targetPos.position, 0.3f));
        _seq.AppendInterval(0.5f);
        _seq.Append(transform.DOMove(_originPos, 0.3f));
    }

}
