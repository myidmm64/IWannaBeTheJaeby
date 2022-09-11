using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

public class PencilBoss : Boss
{
    [SerializeField]
    private Achievements _achievements = null;

    [SerializeField]
    private Transform _movePos;
    private bool _isFirst = true;
    private Vector3 _origin = Vector3.zero;
    private Vector3 _endPos = Vector3.zero;
    [SerializeField]
    private float _duration = 0.5f;

    private Sequence _seq = null;
    private Sequence _moveSeq = null;
    [SerializeField]
    private Sprite _eraserSprite = null;
    [SerializeField]
    private GameObject _dalgona = null;


    private void OnEnable()
    {
        Moving();
        BossRoutine();
        transform.Find("Bariler").GetComponent<SpriteRenderer>().enabled = true;
    }

    private void Moving()
    {
        if (_moveSeq != null)
            _moveSeq.Kill();
        _moveSeq = DOTween.Sequence();

        if (_isFirst)
        {
            _isFirst = false;
            _origin = transform.position;
            _endPos = _movePos.position;
        }
        transform.position = _origin;

        _moveSeq.Append(transform.DOMove(_endPos, _duration).SetEase(Ease.Linear)).SetLoops(-1, LoopType.Yoyo);
    }


    public override void ResetBoss()
    {
        StopAllCoroutines();
        transform.DOKill();
        if (_seq != null)
            _seq.Kill();
        if (_moveSeq != null)
            _moveSeq.Kill();
        transform.position = _origin;

    }

    public void DieReset()
    {
        StopAllCoroutines();
        transform.DOKill();
        if (_seq != null)
            _seq.Kill();
        if (_moveSeq != null)
            _moveSeq.Kill();

        transform.position = new Vector3(_origin.x, 0f);
    }

    private void BossRoutine()
    {
        Pattern0();
    }

    private void Pattern0()
    {
        StartCoroutine(ThrowEraser());
    }

    private void Pattern1()
    {
        StartCoroutine(SpawnDalgona());
    }

    private IEnumerator SpawnDalgona()
    {
        GameObject dal = Instantiate(_dalgona, _bossObjectTrm);
        dal.transform.position = new Vector3(-2f, _endPos.y - 0.5f);
        yield return new WaitForSeconds(1f);
        GameObject dal2 = Instantiate(_dalgona, _bossObjectTrm);
        dal2.transform.position = new Vector3(2f, _endPos.y - 0.5f);
        yield return new WaitForSeconds(4.5f);
        Pattern0();
    }

    private IEnumerator ThrowEraser()
    {
        Transform target = Save.Instance.playerMovemant.transform;

        yield return new WaitForSeconds(1f);
        for(int i = 0; i<8; i++)
        {
            Barrage s = PoolManager.Instance.Pop("Barrage") as Barrage;
            s.transform.SetParent(_bossObjectTrm);
            Vector3 dis = (target.position - transform.position).normalized;
            Quaternion rot = Quaternion.AngleAxis(Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg - 90f, Vector3.forward);
            s.transform.SetPositionAndRotation(transform.position, rot);
            s.SetBarrage(12f, new Vector2(1f, 1.4f), new Vector2(0.02f, 0.1f), _eraserSprite);
            s.transform.localScale = Vector3.one;

            CameraManager.instance.CameraShake(4f, 30f, 0.2f);
            yield return new WaitForSeconds(1f);
        }

        Pattern1();
    }
    public void AchievementSet()
    {
        _achievements.Popup("¹®¹æ±¸ÀÇ ´Ü°ñ¼Õ´Ô", "¿¬ÇÊ±ðÀÌ°¡ ²ÇÂ¥!!", 2);
    }
}
