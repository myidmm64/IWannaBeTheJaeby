using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PoolingPair
{
    public PoolableMono prefab;
    public int poolCnt;
}
[CreateAssetMenu(menuName = "SO/System/PoolingList")]
public class PoolingListSO : ScriptableObject
{
    public List<PoolingPair> list = new List<PoolingPair>();

}
