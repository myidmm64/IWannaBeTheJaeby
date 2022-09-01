using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPool : PoolableMono
{
    [SerializeField]
    private float _speed = 7f;
    [SerializeField]
    private float _minY = -5f;

    private void Update()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime);
        if(transform.position.y < _minY)
        {
            PoolManager.Instance.Push(this);
        }
    }

    public override void PopReset()
    {
        transform.position = Vector3.zero;
    }

    public override void PushReset()
    {
        transform.position = Vector3.zero;
    }
}
