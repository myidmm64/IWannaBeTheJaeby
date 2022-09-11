using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalJump : AgentJump
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

    KeySetting _keySetting = null;

    private void Start()
    {
        if (_keySetting == null)
        {
            _keySetting = KeyManager.Instance.keySetting;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(_keySetting.Keys[KeyAction.JUMP]))
        {
            Jump(1f + _acceleration);
            JumpRenewal();
        }
        if (Input.GetKey(_keySetting.Keys[KeyAction.JUMP]))
        {
            _curTime += Time.deltaTime;
            if (_curTime < _timer)
                return;

            _acceleration += Time.deltaTime;
            _acceleration = Mathf.Clamp(_acceleration, 0f, _maxAcceleration);

            if (_acceleration < _maxAcceleration)
                JumpProportion(_accelerationPower);
        }
        if (Input.GetKeyUp(_keySetting.Keys[KeyAction.JUMP]))
        {
            _acceleration = 0f;
            _curTime = 0f;
        }

        /*if (Input.GetKeyDown(KeyCode.V))
        {
            MoreJump();
        }*/
    }

    private void OnEnable()
    {
        JumpReset();
    }

    public void JumpReset()
    {
        /*GroundCheck();

        _rigid.velocity = Vector2.zero;
        if(_isground == false)
        {

            _currentJumpCnt = 1;
            _isDoubleJump = true;
            _isFirstJump = false;
            OnIsGrounded?.Invoke(false);
        }
        else
        {
            _currentJumpCnt = 0;
            _isDoubleJump = false;
            _isFirstJump = true;
            OnIsGrounded?.Invoke(true);
        }*/
        _currentJumpCnt = 0;
        _isDoubleJump = false;
        _isFirstJump = true;
        _curTime = 0f;
        _acceleration = 0f;
        //OnIsGrounded?.Invoke(false);
    }

    public void MoreJump()
    {
        if (_currentJumpCnt == _jumpCount)
        {
            _currentJumpCnt--;
            _isDoubleJump = true;
        }
    }
}
