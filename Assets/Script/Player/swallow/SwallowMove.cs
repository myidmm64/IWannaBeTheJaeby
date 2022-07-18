using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwallowMove : AgentMovement
{

    private bool _isSwallowMove = true;
    [SerializeField]
    private Transform _swallowTrm = null;
    [SerializeField]
    private float _followSpeed = 3f;

    private Vector2 _followDir = Vector2.zero;

    protected override void FixedUpdate()
    {
        if (_isStop) return;

        _rigid.velocity = new Vector2(_moveDir.x , _moveDir.y ).normalized * (_isSwallowMove ? _speed : _followSpeed) ;
    }

    protected override void Update()
    {
        base.Update();

        if(_isSwallowMove)
        {
            _moveDir.x = Input.GetAxisRaw("Horizontal");
            _moveDir.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            _moveDir = _swallowTrm.position - transform.position;
            if(Mathf.Abs(_moveDir.x) < 0.1f && Mathf.Abs(_moveDir.y) < 0.1f)
            {
                _moveDir = Vector2.zero;
            }
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            _isSwallowMove = !_isSwallowMove;
        }
    }

    public void SwallowPositionReset()
    {
        transform.localPosition = _swallowTrm.localPosition;
    }
}
