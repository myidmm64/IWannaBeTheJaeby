using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapMove : PoolableMono
{
    private float _speed = 0f;
    public float Speed
    {
        get => _speed;
        set
        {
            _speed = value;
        }
    }
    private Rigidbody2D _rigid = null;
    [SerializeField]
    private LayerMask _layerMask = 0;

    private void Awake()
    {
        if (_rigid == null)
        {
            _rigid = GetComponent<Rigidbody2D>();
        }
    }

    private void FixedUpdate()
    {
        _rigid.velocity = Vector2.down * _speed;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, _layerMask);
        if (hit.collider != null)
        {
            Push();
        }
    }

    public override void Reset()
    {
        _speed = 0f;
        _rigid.velocity = Vector2.zero;
        transform.position = Vector3.zero;
    }

    public void Push()
    {
        if(gameObject.activeSelf == true)
            PoolManager.Instance.Push(this);
    }
}
