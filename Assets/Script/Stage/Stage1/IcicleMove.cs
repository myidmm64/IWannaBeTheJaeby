using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IcicleMove : MonoBehaviour
{
    [SerializeField]
    private float _maxSize = 1.5f;
    [SerializeField]
    private float _duration = 1f;
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private Transform _startRay = null;
    [SerializeField]
    private LayerMask _layerMask = 0;
    [SerializeField]
    private AudioClip _brokeClip = null;
    private Sequence _seq = null;
    private Animator _animator = null;
    private Transform _parentTransform = null;
    private Rigidbody2D _rigid = null;
    private bool _playing = false;

    private void OnEnable()
    {
        if(_animator == null)
        {
            _animator = GetComponent<Animator>();
            _parentTransform = transform.parent;
            _rigid = GetComponent<Rigidbody2D>();
        }
        ResetIcicle();
    }

    private void StartIcicle()
    {
        _seq = DOTween.Sequence();
        _seq.Append(_parentTransform.DOScale(Vector3.one * _maxSize, _duration).SetEase(Ease.Linear));
        _seq.AppendCallback(() =>
        {
            Move();
        });
    }

    private void Move()
    {
        _rigid.velocity = Vector2.down * _speed;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(_startRay.position, Vector2.down, 0.3f, _layerMask);
        if(hit.collider != null)
        {
            if (_playing) return;
            _playing = true;

            _rigid.velocity = Vector2.zero;
            _animator.SetTrigger("Thunder");
            AudioPoolable a = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
            a.Play(_brokeClip);
        }
    }

    public void ResetIcicle()
    {
        if (_seq != null)
            _seq.Kill();
        _parentTransform.localScale = Vector3.one;
        _rigid.velocity = Vector2.zero;
        transform.localPosition = Vector3.up * -0.3f;
        _playing = false;
        StartIcicle();
    }
}
