using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : AgentJump
{
    [SerializeField]
    private float _speed = -3f;
    private Vector3 _originPos = Vector3.zero;

    private void Start()
    {
        _originPos = transform.position;
    }

    private void FixedUpdate()
    {
        _rigid.velocity = new Vector2(_speed ,_rigid.velocity.y);
    }

    protected override void Update()
    {
        base.Update();
        if(_isground)
        {
            ForceJump(Random.Range(_jumpPower * 0.9f, _jumpPower), Vector3.up);
        }
    }

    private void OnDisable()
    {
        transform.position = _originPos;
    }
}
