using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private PoolingListSO _initList = null;

    private void Awake()
    {
        PoolManager.Instance = new PoolManager(transform);

        CreatePool();
    }

    private void CreatePool()
    {
        foreach(PoolingPair pair in _initList.list)
        {
            PoolManager.Instance.CreatePool(pair.prefab, pair.poolCnt);
        }
    }
}
