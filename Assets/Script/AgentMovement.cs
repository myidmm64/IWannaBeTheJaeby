using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentMovement : MonoBehaviour
{
    [SerializeField]
    protected float _speed = 5f;
    [SerializeField]
    private float _dashPower = 10f;
    [SerializeField]
    private float _rigidDrag = 20f;
    [SerializeField]
    [Range(0.2f, 2f)]
    private float _dashInterval = 0.3f;

    protected bool _isStop = false;

    protected Rigidbody2D _rigid = null;

    protected Vector2 _moveDir = Vector2.zero;

    [field:SerializeField]
    private UnityEvent<float> OnvelocityChange; //속도 바뀔 때 실행될 이벤트
    [field: SerializeField]
    private UnityEvent<float> OnJumpVelocityChange; //속도 바뀔 때 실행될 이벤트

    [field: SerializeField]
    private UnityEvent OnDashStartEvent; 
    [field: SerializeField]
    private UnityEvent OnDashEndEvent;

    [SerializeField]
    protected bool _dashable = true;
    private Coroutine _dashCoroutine = null;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        if (_isStop) return;

        _rigid.velocity = new Vector2(_moveDir.x * _speed, _rigid.velocity.y);
    }

    protected virtual void Update()
    {
        if (_isStop) return;

        OnvelocityChange?.Invoke(_moveDir.x * _speed);
        OnJumpVelocityChange?.Invoke(_rigid.velocity.y);
    }

    public void VelocityStopImmediately()
    {
        _rigid.velocity = Vector2.zero;
    }

    public void Dash(Vector2 dir)
    {
        if (_dashable == false)
            return;

        if (_dashCoroutine != null)
            StopCoroutine(_dashCoroutine);
        _dashCoroutine = StartCoroutine(DashCooltime());

        VelocityStopImmediately();
        _rigid.AddForce(dir * _dashPower, ForceMode2D.Impulse);

        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCooltime()
    {
        _dashable = false;
        yield return new WaitForSeconds(_dashInterval);
        _dashable = true;
    }

    public void MovementDieReset()
    {
        if (_dashCoroutine != null)
            StopCoroutine(_dashCoroutine);
        StopCoroutine("DashCoroutine");

        _isStop = false;
        _rigid.gravityScale = 4.8f;
        _rigid.drag = 0.5f;
        OnDashEndEvent?.Invoke();
        //_dashable = true;
    }

    private IEnumerator DashCoroutine()
    {
        float gravity = _rigid.gravityScale;
        float drag = _rigid.drag;

        _isStop = true;
        _rigid.gravityScale = 0f;
        _rigid.drag = _rigidDrag;
        OnDashStartEvent?.Invoke();

        yield return new WaitForSeconds(0.2f);

        _isStop = false;
        _rigid.gravityScale = gravity;
        _rigid.drag = drag;
        OnDashEndEvent?.Invoke();
    }

}
