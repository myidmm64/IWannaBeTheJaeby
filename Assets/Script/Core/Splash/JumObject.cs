using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumObject : MonoBehaviour
{
    [SerializeField]
    private float _jumpPower = 2f;
    [SerializeField]
    private int _jumpnum = 1;
    [SerializeField]
    private float _duration = 0.5f;

    private void Start()
    {
        transform.DOJump(transform.position, _jumpPower, _jumpnum, _duration).SetEase(Ease.Linear);
    }
}
