using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField]
    private Transform _movePos;
    private bool _isFirst = true;
    private Vector3 _origin = Vector3.zero;
    private Vector3 _endPos = Vector3.zero;
    [SerializeField]
    private float _duration = 0.5f;


    private void OnEnable()
    {
        if(_isFirst)
        {
            _isFirst = false;
            _origin = transform.position;
            _endPos = _movePos.position;
        }

        transform.DOKill();
        transform.DOMove(_endPos, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        transform.position = _origin;
    }
}
