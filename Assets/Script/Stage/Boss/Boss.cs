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
        ResetBoss();
        DestroyTrash();
    }

    public virtual void DestroyTrash()
    {
        for (int i = 0; i < _bossObjectTrm.childCount; i++)
        {
            PoolableMono mono = _bossObjectTrm.GetChild(i).GetComponent<PoolableMono>();
            if (mono != null)
            {
                PoolManager.Instance.Push(_bossObjectTrm.GetChild(i).GetComponent<PoolableMono>());
            }
            else
            {
                Destroy(_bossObjectTrm.GetChild(i));
            }
        }
    }

    public abstract void ResetBoss();
}
