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
    [SerializeField]
    private LineRenderer _lineRenderer = null;
    [SerializeField]
    private GameObject _ragerAttack;
    [SerializeField]
    private Material _ragerAttackMaterial;


    private void OnEnable()
    {
        Moving();
        BossRoutine();
        transform.Find("Bariler").GetComponent<SpriteRenderer>().enabled = true;
        _lineRenderer.enabled = false;
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
        StartCoroutine(Rager());
    }

    private void Pattern2()
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
    private IEnumerator Rager()
    {
        Transform target = Save.Instance.playerMovemant.transform;
        float time = 0.4f;
        _moveSeq.Pause();
        _lineRenderer.enabled = true;
        _ragerAttackMaterial.SetFloat("_Alpha", 1f);
        _ragerAttackMaterial.SetColor("_Color", Color.red);
        _lineRenderer.startColor = Color.red; 
        _lineRenderer.endColor = Color.red;

        while(time > 0f)
		{
            time -= 0.01f;
            _lineRenderer.startWidth = (1f - time) * 0.05f;
            _lineRenderer.endWidth = (1f - time) * 0.05f;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, target.transform.position);
            yield return new WaitForSeconds(0.01f);
        }
        GameObject dal = Instantiate(_ragerAttack, null);
        dal.SetActive(false);
        dal.transform.position = target.transform.position;
        yield return new WaitForSeconds(0.05f);
        _ragerAttackMaterial.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(0.05f);
        _ragerAttackMaterial.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(0.05f);
        _ragerAttackMaterial.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(0.05f);
        _ragerAttackMaterial.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(0.05f);
        _ragerAttackMaterial.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(0.3f);
        _lineRenderer.startWidth = 2f;
        _lineRenderer.endWidth = 2f;
        _ragerAttackMaterial.DOFloat(50f, "_Alpha", 0.2f);
        dal.SetActive(true);
        Destroy(dal, 0.3f);
        yield return new WaitForSeconds(0.15f);
        _ragerAttackMaterial.DOFloat(1f, "_Alpha", 0.1f);
        yield return new WaitForSeconds(0.1f);
        _lineRenderer.enabled = false;
        _moveSeq.Play();


        Pattern2();
    }


    public void AchievementSet()
    {
        _achievements.Popup("¹®¹æ±¸ÀÇ ´Ü°ñ¼Õ´Ô", "¿¬ÇÊ±ðÀÌ°¡ ²ÇÂ¥!!", 2);
    }
}
