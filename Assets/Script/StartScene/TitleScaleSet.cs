using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleScaleSet : MonoBehaviour
{
    private Vector3 _originScale = Vector3.zero;
    [SerializeField]
    private float _multyple = 1.5f;
    [SerializeField]
    private float _duration = 0.2f;

    private void Start()
    {
        _originScale = transform.localScale;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(_originScale * _multyple, _duration).SetEase(Ease.Linear));
        seq.AppendInterval(0.5f);
        seq.Append(transform.DOScale(_originScale, _duration).SetEase(Ease.Linear)).SetLoops(-1, LoopType.Yoyo);
    }
}
