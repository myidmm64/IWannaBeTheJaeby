using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentJump : MonoBehaviour
{
    private Rigidbody2D _rigid;
    [SerializeField]
    protected float _jumpPower = 10f;
    [SerializeField]
    protected float _secondJumpPower = 0.8f;
    [SerializeField]
    private float _rayLength = 0.5f;
    [SerializeField]
    protected int _jumpCount = 2;
    protected int _currentJumpCnt = 0;

    private bool _isJumpable = true;
    protected bool _isDoubleJump = false;
    private bool _isFirstJump = true;

    [SerializeField]
    private Transform[] _groundRayStartPos = null;
    protected bool _isground = false;

    [field: SerializeField]
    private UnityEvent OnJumpPress = null;
    [field: SerializeField]
    private UnityEvent<bool> OnIsGrounded = null;

    [SerializeField]
    private LayerMask _groundMask = 0;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        GroundCheck();
    }

    public void GroundCheck()
    {
        Collider2D col = null;

        for(int i = 0; i< _groundRayStartPos.Length; i++)
        {
            Debug.DrawRay(_groundRayStartPos[i].position, _groundRayStartPos[i].up * -1f * _rayLength, Color.blue);

            RaycastHit2D hit = Physics2D.Raycast(_groundRayStartPos[i].position, _groundRayStartPos[i].up * -1f, _rayLength, _groundMask);

            if (hit.collider != null && hit.point.y < _groundRayStartPos[0].transform.position.y)
            {
                col = hit.collider;
            }
        }

        if (col != null)
        {
            _isground = true;
            OnIsGrounded?.Invoke(true);
            _isFirstJump = true;
            _currentJumpCnt = 0;
        }
        else if (col == null)
        {
            _isground = false;
            OnIsGrounded?.Invoke(false);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if(collision.collider.CompareTag("Ground"))
        {
            _isground = true;
            OnIsGrounded?.Invoke(true);
            _currentJumpCnt = 0;
        }*/
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        /*if (collision.collider.CompareTag("Ground"))
        {
            _isground = false;
            OnIsGrounded?.Invoke(false);
            _currentJumpCnt = 1;
        }*/

        if(_isFirstJump)
        {
            _currentJumpCnt = 1;
        }
    }

    public void Jump(float accelerationJumpPower = 1f)
    {
        if (_isJumpable == false)
            return;
        if (_isground == false && _currentJumpCnt >= _jumpCount)
            return;

        _isFirstJump = false;
        float jumpPow = _currentJumpCnt > 0 ? _jumpPower * _secondJumpPower * accelerationJumpPower : _jumpPower * accelerationJumpPower;
        if (_isDoubleJump)
        {
            jumpPow = _jumpPower * _secondJumpPower;
            Debug.Log("´õºí!!");
        }

        OnJumpPress?.Invoke();

        _rigid.velocity = new Vector2(_rigid.velocity.x, 0f);
        _rigid.AddForce(Vector3.up * jumpPow, ForceMode2D.Impulse);

        _isDoubleJump = false;
    }

    public void JumpProportion(float power)
    {
        if (_isJumpable == false)
            return;
        if (_isground == false && _currentJumpCnt > _jumpCount)
            return;

        if (_currentJumpCnt > 1)
            power *= _secondJumpPower;

        _rigid.AddForce(Vector3.up * (_jumpPower * power) * Time.deltaTime, ForceMode2D.Force);
    }

    public void JumpRenewal()
    {
        if (_isJumpable == false)
            return;
        if (_isground == false && _currentJumpCnt >= _jumpCount)
            return;

        _currentJumpCnt++;
        //Debug.Log($"JumpCount : {_currentJumpCnt}");
    }

    public void JumpEnable()
    {
        _isJumpable = true;
        //Debug.Log($"À¸¾Ó");
    }

    public void JumpDisable()
    {
        _isJumpable = false;
        //Debug.Log($"±¸¿Í¾Ç");
    }
}
