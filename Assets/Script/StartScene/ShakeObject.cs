using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeObject : MonoBehaviour
{
    [SerializeField]
    private float _duration = 0.5f;
    [SerializeField]
    private int _viv = 10;
    [SerializeField]
    private float _str = 1f;

    private void Start()
    {
        transform.DOShakePosition(_duration, _str, _viv).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
