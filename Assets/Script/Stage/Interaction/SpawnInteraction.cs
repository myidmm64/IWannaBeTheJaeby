using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInteraction : Interaction
{
    [SerializeField]
    private SpawnObjectInfo[] _spawnObjectInfo = null;
    private bool _isFirst = true;

    public override void DoEnterInteraction()
    {
        SpawnObjects();
    }

    public override void DoExitInteraction()
    {
    }

    public override void DoStayInteraction()
    {
    }

    private void OnEnable()
    {
        if(_isFirst)
        {
            _isFirst = false;
            for(int i = 0; i < _spawnObjectInfo.Length; i++)
            {
                if (_spawnObjectInfo[i].obj != null)
                {
                    _spawnObjectInfo[i].originPos = _spawnObjectInfo[i].obj.transform.position;
                }
            }
        }
    }
    private void OnDisable()
    {

        for (int i = 0; i < _spawnObjectInfo.Length; i++)
        {
            if (_spawnObjectInfo[i].obj != null)
            {
                _spawnObjectInfo[i].obj.transform.position = _spawnObjectInfo[i].originPos;
                _spawnObjectInfo[i].obj.SetActive(false);
            }
        }
    }

    private void SpawnObjects()
    {
        for(int i = 0; i<_spawnObjectInfo.Length; i++)
        {
            if (_spawnObjectInfo[i].obj != null)
            {
                _spawnObjectInfo[i].obj.SetActive(true);
            }

            else if (_spawnObjectInfo[i].spawnObj != null)
            {
                for(int j = 0; j < _spawnObjectInfo[i].spawnCount; j++)
                {
                    Instantiate(_spawnObjectInfo[i].spawnObj, _spawnObjectInfo[i].trm);
                }
            }
        }
    }
}
[System.Serializable]
public struct SpawnObjectInfo
{
    [HideInInspector]
    public Vector3 originPos;
    public GameObject obj;
    public GameObject spawnObj;
    public Transform trm;
    public int spawnCount;
}
