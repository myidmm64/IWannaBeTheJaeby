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
