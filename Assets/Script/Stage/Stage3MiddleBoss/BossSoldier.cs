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
    private SoldierUIManager _soldierUIManager = null;

    private Sequence _seq = null;

    private void OnEnable()
    {
        if( _soldierUIManager == null)
            _soldierUIManager = GetComponent<SoldierUIManager>();
        BossRoutine();
        _firstGroundTile.SetActive(true);
    }

    private void BossRoutine()
    {
        Pattern0();
    }

    private void Pattern0()
    {
        StartCoroutine(Pattern0Coroutine());
    }

    private IEnumerator Pattern0Coroutine()
    {
        _soldierUIManager.SetText("모든 적이", "1초 뒤에", "죽는다", 1f, 1f, () =>
        {
            for (int i = 0; i < _enemysTrm.childCount; i++)
            {
                _enemysTrm.GetChild(i).Find("AgentSprite").GetComponent<EnemyDamage>().Damaged();
            }
            _firstGroundTile.SetActive(false);
        });
        yield return new WaitForSeconds(2f);
    }

    public override void ResetBoss()
    {
        StopAllCoroutines();
        if (_seq != null)
            _seq.Kill();
    }
}
