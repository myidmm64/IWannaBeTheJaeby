using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BulletMove : PoolableMono
{
    [SerializeField]
    private float _speed = 2f;
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    private Vector2 _boundX = Vector2.zero;
    private Vector2 _boundY = Vector2.zero;
    private Camera _cam = null;

    [SerializeField]
    private bool _isEnemy = false;

    protected virtual void Awake()
    {
        _cam = Maincam;
    }

    protected virtual void Start()
    {
        Vector3 worldToScreen = _cam.ScreenToWorldPoint(new Vector3(Screen.currentResolution.width, Screen.currentResolution.height, 0f));
        _boundX = new Vector2(worldToScreen.x ,  worldToScreen.x * -1f); // ÁÂ¿ì
        _boundY = new Vector2(worldToScreen.y, worldToScreen.y * -1f); // »óÇÏ
    }


    private void Update()
    {
        Move();

        if(transform.position.x < _boundX.y || transform.position.x > _boundX.x || transform.position.y > _boundY.x || transform.position.y < _boundY.y)
        {
            PoolManager.Instance.Push(this);
        }
    }

    protected virtual void Move()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Check(collision);
    }

    protected virtual void Check(Collider2D collision)
    {
        if (_isEnemy == false)
        {
            if (collision.CompareTag("Player") == true)
            {
                return;
            }
            if (collision.CompareTag("Swallow") == true || collision.CompareTag("Interaction") == true || collision.CompareTag("PlayerAtk") == true)
            {
                return;
            }

            else
            {
                PoolManager.Instance.Push(this);
            }
        }
        else
        {
            /*else
            {
                PoolManager.Instance.Push(this);
            }*/
        }
    }

    public override void PopReset()
    {
    }

    protected virtual void ChildReset()
    {

    }

    public override void PushReset()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        ChildReset();
    }
}
