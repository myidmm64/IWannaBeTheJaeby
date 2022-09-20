using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    public static PoolManager Instance;

    public Dictionary<string, Pool<PoolableMono>> _pools = new Dictionary<string, Pool<PoolableMono>>();

    private Transform _trmParent;

    public PoolManager(Transform trmParent)
    {
        _trmParent = trmParent;
        Instance = this;
    }
    public void CreatePool(PoolableMono prefab, int count = 10)
    {
        Pool<PoolableMono> pool = new Pool<PoolableMono>(prefab, _trmParent, count);
        _pools.Add(prefab.gameObject.name,pool);
    }

    public PoolableMono Pop(string prefabName)
    {
        //Debug.Log(prefabName);
        if (!_pools.ContainsKey(prefabName))
        {
            Debug.Log("Prefab dosnt exist on pool");
            return null;
        }
        PoolableMono item = _pools[prefabName].Pop();
        item.PopReset();
        return item;
    }
    public void Push(PoolableMono obj)
    {
        //앞뒤공백만지워주는 trim
        obj.PushReset();
        obj.transform.SetParent(_trmParent);
        _pools[obj.name.Trim()].Push(obj);
    }
}
