using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pool<T> where T : PoolableMono //where -> T에 대한 조건을 설정 ^^
{
    private Stack<T> _pool = new Stack<T>();
    private T _prefab;
    private Transform _parent;
    public Pool(T prefab, Transform parent, int count)
    {
        _prefab = prefab;
        _parent = parent;

        for(int i =0; i<count; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
        }
    }
    public T Pop()
    {
        T obj = null;
        if(_pool.Count <= 0)
        {
            obj = GameObject.Instantiate(_prefab, _parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            obj = _pool.Pop(); // 맨 꼭대기 있는 노석 가져오기
            obj.gameObject.SetActive(true);
        }
        return obj;
    }
    public void Push(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Push(obj);
    }
}
