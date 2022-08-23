using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : PoolableMono
{
    [SerializeField]
    private float _speed = 2f;
    private Vector2 _bound = Vector2.zero;
    private Camera _cam = null;

    [SerializeField]
    private bool _isEnemy = false;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Start()
    {
        Vector3 worldToScreen = _cam.ScreenToWorldPoint(new Vector3(Screen.currentResolution.width, Screen.currentResolution.height, 0f));
        _bound = new Vector2(worldToScreen.x ,  worldToScreen.x * -1f);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);

        if(transform.position.x < _bound.y || transform.position.x > _bound.x)
        {
            PoolManager.Instance.Push(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_isEnemy == false)
        {
            if(collision.CompareTag("Player") == true)
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

    public override void Reset()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}
