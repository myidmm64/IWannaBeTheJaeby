using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentAccalerationMovement : MonoBehaviour
{
    private Rigidbody2D _rigid;

    protected float _currentVelocity = 3;
    protected Vector2 _movementDirection;

    private Vector2 _testDir = Vector2.zero;

    [SerializeField]
    private float _accaleration = 3f;
    [SerializeField]
    private float _deAcceleration = 3f;
    [SerializeField]
    private float _maxspeed = 10f;

    [field: SerializeField]
    private UnityEvent<float> OnvelocityChange; //속도 바뀔 때 실행될 이벤트

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void MoveAgent(Vector2 movementInput)
    {
        if (movementInput.sqrMagnitude > 0)
        {
            //가고자 하는 방향과 반대면 가속 중지
            if (Vector2.Dot(movementInput, _movementDirection) < 0)
            {
                _currentVelocity = 0;
            }
            _movementDirection = movementInput.normalized;
        }
        _currentVelocity = CalculateSpeed(movementInput);
    }

    private float CalculateSpeed(Vector2 movementInput)
    {
        if (movementInput.sqrMagnitude > 0)
        {
            _currentVelocity += _accaleration * Time.deltaTime;
        }
        else
        {
            _currentVelocity -= _deAcceleration * Time.deltaTime;
        }
        return Mathf.Clamp(_currentVelocity, 0, _maxspeed);
    }

    private void FixedUpdate()
    {
        OnvelocityChange?.Invoke(_currentVelocity);

        _testDir.x = Input.GetAxisRaw("Horizontal");
        MoveAgent(_testDir);
        _rigid.velocity = _testDir * _currentVelocity;
        //_rigid.velocity = _movementDirection * _currentVelocity;
    }
}