using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAndPushPoolable : PoolableMono
{
    public override void Reset()
    {
        StopAllCoroutines();
        transform.position = Vector3.zero;
    }

    public void Push(float time)
    {
        StartCoroutine(WaitCoroutine(time));
    }

    private IEnumerator WaitCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        PoolManager.Instance.Push(this);
    }
}
