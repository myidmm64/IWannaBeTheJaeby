using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : PoolableMono
{
    private Animator _animator = null;

    public void StartAnimation()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();

        _animator.SetTrigger("Thunder");
    }

    public void EndAnimation()
    {
        PoolManager.Instance.Push(this);
    }

    public override void PopReset()
    {
    }

    public override void PushReset()
    {
        transform.position = Vector3.zero;
        transform.localScale = Vector3.one;
    }
}
