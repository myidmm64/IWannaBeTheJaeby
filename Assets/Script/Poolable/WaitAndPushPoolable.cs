using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAndPushPoolable : PoolableMono
{
    public override void PopReset()
    {
        StopAllCoroutines();
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public void Push(float time)
    {
        StartCoroutine(WaitCoroutine(time));
    }

    public override void PushReset()
    {
    }

    private IEnumerator WaitCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        PoolManager.Instance.Push(this);
    }

    public void PushImm()
    {
        PoolManager.Instance.Push(this);
    }
}
