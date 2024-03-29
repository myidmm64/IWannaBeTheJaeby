using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwallowMove : AgentMovement
{

    private bool _isSwallowMove = false;
    [SerializeField]
    private Transform _swallowTrm = null;
    [SerializeField]
    private float _followSpeed = 3f;
    [SerializeField]
    private float _swallowTime = 0f;
    private bool _swallowable = true;

    private Vector2 _followDir = Vector2.zero;
    private SpriteRenderer _spriteRenderer = null;

    [SerializeField]
    private GameObject _player = null;
    private PlayerAttack _playerAttack = null;
    private PlayerNormalJump _playerJumps = null;
    private PlayerMovement _playerMove = null;
    private Rigidbody2D _playerRigid = null;
    [field: SerializeField]
    private UnityEvent OnSwallowMode = null;
    private Collider2D _col = null;
    [field: SerializeField]
    private UnityEvent OnSwallowDie = null;
    [SerializeField]
    private PlayerDamage _playerDamage = null;
    private TrailRenderer _trail = null;

    private void Awake()
    {
        _trail = transform.Find("GameObject").GetComponent<TrailRenderer>();
        _col = GetComponent<Collider2D>();
        _col.enabled = false;

        _playerRigid = _player.GetComponent<Rigidbody2D>();
        _playerAttack = _player.GetComponent<PlayerAttack>();
        _playerJumps = _player.GetComponent<PlayerNormalJump>();
        _playerMove = _player.GetComponent<PlayerMovement>();
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

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

        if(Input.GetKeyDown(KeyCode.S))
        {
            if (_swallowable == false) return;

            OnSwallowMode?.Invoke();

            SwallowModeSet(true);
        }
    }

    public void SwallowPositionReset()
    {
        if (gameObject.activeSelf == false) return;

        transform.localPosition = _swallowTrm.localPosition;
    }

    public void FaceDiractionChange(float amount)
    {
        if(amount < 0f)
        {
            _spriteRenderer.flipX = true;
        }
        if(amount > 0f)
        {
            _spriteRenderer.flipX = false;
        }
        if(_isSwallowMove == false)
        {
            if(amount == 0f)
            {
                _spriteRenderer.flipX = false;
            }
        }
    }

    public void SwallowModeSet(bool val)
    {
        if (gameObject.activeSelf == false) return;
        if (_swallowable == false) return;

        if (val)
        {
            _trail.enabled = true;
            _playerRigid.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            StartCoroutine(SwallowCoroutine());
        }
        else
        {
            _trail.enabled = false;
            _playerRigid.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.None;
        }

        _playerRigid.velocity = Vector2.zero;
        _playerAttack.enabled = !val;
        _playerJumps.enabled = !val;
        _playerMove.enabled = !val;
        _col.enabled = val;
        _isSwallowMove = val;
    }

    private IEnumerator SwallowCoroutine()
    {
        yield return new WaitForSeconds(_swallowTime);
        SwallowModeSet(false);
        _swallowable = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Die"))
        {
            _swallowable = true;
            OnSwallowDie?.Invoke();
            _playerDamage.Die();
        }
    }
}
