using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossSoldier : Boss
{
    [SerializeField]
    private Transform _enemysTrm = null;
    [SerializeField]
    private GameObject _firstGroundTile = null;

    private Sequence _seq = null;

    private void OnEnable()
    {
        BossRoutine();
        _firstGroundTile.SetActive(true);
    }

    private void BossRoutine()
    {
        StartCoroutine(FirstCoroutine());
    }

    private IEnumerator FirstCoroutine()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < _enemysTrm.childCount; i++)
        {
            _enemysTrm.GetChild(i).Find("AgentSprite").GetComponent<EnemyDamage>().Damaged();
        }
        _firstGroundTile.SetActive(false);
        Pattern0();
    }

    private void Pattern0()
    {

    }

    public override void ResetBoss()
    {
        StopAllCoroutines();
        if (_seq != null)
            _seq.Kill();
    }
}
