using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    [SerializeField]
    protected Transform _bossObjectTrm = null;

    private void OnDisable()
    {
        DestroyTrash();
        ResetBoss();
    }

    public virtual void DestroyTrash()
    {
        PoolableMono[] monos = _bossObjectTrm.GetComponentsInChildren<PoolableMono>();
        for(int i = 0; i<monos.Length; i++)
        {
            PoolManager.Instance.Push(monos[i]);
        }
        int count = _bossObjectTrm.childCount;
        for(int i = 0; i< count; i++)
        {
            Destroy(_bossObjectTrm.GetChild(i).gameObject);
        }
    }

    public abstract void ResetBoss();
}
