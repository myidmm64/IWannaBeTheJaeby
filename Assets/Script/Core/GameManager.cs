using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PoolingListSO _initList = null;

    [SerializeField]
    private UnityEvent OnExit = null;

    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get => _instance;
    }

    private void Awake()
    {
        PoolManager.Instance = new PoolManager(transform);
        if (_instance == null)
            _instance = this;
        CreatePool();
    }

    private void CreatePool()
    {
        foreach(PoolingPair pair in _initList.list)
        {
            PoolManager.Instance.CreatePool(pair.prefab, pair.poolCnt);
        }
    }

    public void PoolAdd(PoolableMono prefab, int poolCnt)
    {
        PoolManager.Instance.CreatePool(prefab, poolCnt);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            DOTween.KillAll();
            OnExit?.Invoke();
            SceneManager.LoadScene(0);
        }
    }
}
