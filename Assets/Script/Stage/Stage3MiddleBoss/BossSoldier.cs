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
    private Vector3 _originPos = Vector3.zero;

    private Sequence _seq = null;

    private void Start()
    {
        _originPos = transform.position;
    }

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
        _soldierUIManager.SetText("모든 적이", "1초 뒤에", "죽는다", 1f, 1f, () =>
        {
            for (int i = 0; i < _enemysTrm.childCount; i++)
            {
                _enemysTrm.GetChild(i).Find("AgentSprite").GetComponent<EnemyDamage>().Damaged();
            }
            _firstGroundTile.SetActive(false);
            if (_seq != null)
                _seq.Kill();
            _seq = DOTween.Sequence();
            _seq.Append(transform.DOMoveY(3f, 1f));
            _seq.AppendCallback(() =>
            {
                Pattern1();
            });
        });
    }

    private void Pattern1()
    {
        _soldierUIManager.SetText("플레이어가", "1초마다", "뛰어오른다", 1f, 1f, ()=>
        {

        });
    }

    public override void ResetBoss()
    {
        StopAllCoroutines();
        if (_seq != null)
            _seq.Kill();
        transform.position = _originPos;
    }
}
