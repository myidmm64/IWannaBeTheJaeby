using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private float _rayLength = 0.5f;
    [SerializeField]
    private float _targetingSpeed = 3f;

    [SerializeField]
    private bool _isLeft = false;
    [SerializeField]
    private Transform _startRay = null;
    [SerializeField]
    private LayerMask _mapMask = 0;
    [SerializeField]
    private LayerMask wallMask = 0;

    private Vector3 _startPosision = Vector2.zero;
    private bool _isfirst = true;

    private SpriteRenderer _spriteRenderer = null;
    private Rigidbody2D _rigid = null;
    private Animator _animator = null;
    private Collider2D _col = null;

    private Vector3 _localScale = Vector3.zero;
    private Vector2 _moveDir;

    private Transform _target = null;
    private bool _targeting = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
        _localScale = transform.localScale;
        ResetDir();
    }

    private void OnEnable()
    {
        ObjectReset();
        ResetDir();
    }

    private void FixedUpdate()
    {
        _rigid.velocity = _moveDir * _speed;
    }

    private void Update()
    {
        if (_targeting && _target != null)
        {
            TartgetMove();
        }
        else
        {
            Move();
        }
    }

    public void TargetSet(Collider2D col)
    {
        if(_animator == null)
            _animator = transform.Find("AgentSprite").GetComponent<Animator>();
        _target = col.transform;
        _targeting = true;
        _animator.SetBool("Fly", true);
    }

    private void ResetDir()
    {
        if (_isLeft)
        {
            _moveDir = Vector2.left;
            _localScale.x = Mathf.Abs(_localScale.x) * -1f;
            transform.localScale = _localScale;
        }
        else
        {
            _moveDir = Vector2.right;
            _localScale.x = Mathf.Abs(_localScale.x);
            transform.localScale = _localScale;
        }
    }
    private void ObjectReset()
    {
        if (_isfirst)
        {
            _startPosision = transform.position;
            _isfirst = false;
        }

        transform.position = _startPosision;
        _targeting = false;
        _target = null;

        if (_col != null)
            _col.enabled = true;
    }
    private void TartgetMove()
    {
        _col.enabled = false;
        _moveDir = (_target.position - transform.position).normalized;
        if (_target.position.x < transform.position.x) // ¿À¸¥ÂÊ
        {
            _localScale.x = Mathf.Abs(_localScale.x) * -1f;
            transform.localScale = _localScale;
        }
        else
        {
            _localScale.x = Mathf.Abs(_localScale.x);
            transform.localScale = _localScale;
        }
    }
    private void Move()
    {
        RaycastHit2D hit = Physics2D.Raycast(_startRay.position, Vector2.down, _rayLength, _mapMask);
        if (hit.collider == null)
        {
            _localScale.x *= -1f;
            _moveDir *= -1f;
            transform.localScale = _localScale;
        }

        hit = Physics2D.Raycast(_startRay.position, Vector2.right, _rayLength, wallMask);
        if (hit.collider != null)
        {
            _localScale.x *= -1f;
            _moveDir *= -1f;
            transform.localScale = _localScale;
        }
    }
}
