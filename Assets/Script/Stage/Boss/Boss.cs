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
        List<GameObject> destroyList = new List<GameObject>();
        PoolableMono mono = null;

        int count = _bossObjectTrm.childCount;
        for (int i = 0; i < count; i++)
        {
            mono = _bossObjectTrm.GetChild(i).GetComponent<PoolableMono>();
            if (mono != null)
            {
                PoolManager.Instance.Push(mono);
            }
            else
            {
                destroyList.Add(_bossObjectTrm.GetChild(i).gameObject);
            }
        }

        if (destroyList.Count > 0)
            for (int i = 0; i < destroyList.Count; i++)
            {
                Destroy(destroyList[i]);
            }
    }

    public abstract void ResetBoss();
}
