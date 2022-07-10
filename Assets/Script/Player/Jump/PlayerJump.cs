using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : AgentJump
{
    [SerializeField]
    private float _maxAcceleration = 1f; // 최대로 높이 뛰기
    [SerializeField]
    private float _accelerationPower = 0.06f;

    [SerializeField]
    private float _timer = 0.1f;
    [SerializeField]
    private float _curTime = 0f;

    [SerializeField]
    private float _acceleration = 0f;


    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.C))
        {
            Jump(1f + _acceleration);
            JumpRenewal();
        }
        if (Input.GetKey(KeyCode.C))
        {
            _curTime += Time.deltaTime;
            if (_curTime < _timer)
                return;

            _acceleration += Time.deltaTime;
            _acceleration = Mathf.Clamp(_acceleration, 0f, _maxAcceleration);

            if (_acceleration < _maxAcceleration)
                JumpProportion(_accelerationPower);
        }
        if(Input.GetKeyUp(KeyCode.C))
        {
            _acceleration = 0f;
            _curTime = 0f;
        }


        /*if (Input.GetKeyDown(KeyCode.V))
        {
            MoreJump();
        }*/
    }

    public void JumpReset()
    {
        GroundCheck();

        if(_isground == false)
        {
            _currentJumpCnt = 1;
        }
        else
        {
            _currentJumpCnt = 0;
        }
    }

    public void MoreJump()
    {
        Debug.Log("asdsda");
        if(_currentJumpCnt == _jumpCount)
        {
            _currentJumpCnt--;
            _isDoubleJump = true;
        }
    }
}
