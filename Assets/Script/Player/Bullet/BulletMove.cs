using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : PoolableMono
{
    [SerializeField]
    private float _speed = 2f;
    private Vector2 _bound = Vector2.zero;
    private Camera _cam = null;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Start()
    {
        Vector3 worldToScreen = _cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0f));
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
        if(collision.CompareTag("Player") == true || collision.CompareTag("Swallow") == true)
        {
        }
        else
        {
            PoolManager.Instance.Push(this);
        }
    }

    public override void Reset()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}
