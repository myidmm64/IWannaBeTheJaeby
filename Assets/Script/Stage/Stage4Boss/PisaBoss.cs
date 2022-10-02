using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PisaBoss : Boss
{
    [SerializeField]
    private GameObject _skipText = null;


    private Sequence _seq = null;
    private Vector3 _originPos = Vector3.zero;
    private Collider2D _col = null;

    private Vector3 _cycleOriginPos = Vector3.zero;

    [SerializeField]
    private Transform _cutSceneParent = null;
    [SerializeField]
    private GameObject _cutScenePisa = null;
    [SerializeField]
    private GameObject _cutSceneCycle = null;

    [SerializeField]
    private Transform[] _pattrnZeroTrms = null;
    [SerializeField]
    private Transform[] _pattrnZeroTrmsTwo = null;

    private Rigidbody2D _cutSceneCycleRigid = null;
    private SpriteRenderer _spriteRenderer = null;

    private PisaBulletManager _pisaBulletManager = null;

    private Transform _target = null;

    [SerializeField]
    private GameObject _movingFireObj = null;
    [SerializeField]
    private GameObject _dalgonaGift = null;
    [SerializeField]
    private AudioClip _groundPoundClip = null;

    [SerializeField]
    private GameObject _movingObj = null;

    private bool _isSkipable = true;

    private void OnEnable()
    {
        if (_seq != null)
            _seq.Kill();
        if (_originPos == Vector3.zero)
        {
            _originPos = transform.position;
            _cycleOriginPos = _cutSceneCycle.transform.position;
            _col = transform.Find("AgentSprite").GetComponent<Collider2D>();
            _cutSceneCycleRigid = _cutSceneCycle.GetComponent<Rigidbody2D>();
            _spriteRenderer = _col.GetComponent<SpriteRenderer>();
            _pisaBulletManager = GetComponent<PisaBulletManager>();
            _target = Save.Instance.playerMovemant.transform;
        }

        _skipText.SetActive(true);
        _spriteRenderer.enabled = true;
        _col.enabled = true;

        _cutSceneParent.gameObject.SetActive(true);
        _cutSceneParent.transform.position = new Vector3(0f, -8.72f, 0f);
        _cutScenePisa.transform.localPosition = Vector3.zero;
        _cutSceneCycle.transform.position = _cycleOriginPos;
        _cutSceneCycleRigid.velocity = Vector2.zero;
        _cutSceneCycleRigid.gravityScale = 0f;

        CameraManager.instance.CameraShake(6f, 30f, 9.5f, true);
        _seq = DOTween.Sequence();
        _seq.Append(_cutSceneParent.DOMoveY(1.31f, 7.5f));
        _seq.AppendCallback(() =>
        {
            Vector3 randomCir = UnityEngine.Random.insideUnitCircle;
            randomCir.y = Mathf.Abs(randomCir.y);
            _cutSceneCycleRigid.gravityScale = 1f;
            _cutSceneCycleRigid.AddForce(randomCir * 7.5f, ForceMode2D.Impulse);
        });
        _seq.AppendInterval(1f);
        _seq.Append(_cutScenePisa.transform.DOMoveY(13f, 1f));
        _seq.AppendCallback(() =>
        {
            BossRoutine();
        });
    }

    private void BossRoutine()
    {
        _cutSceneParent.gameObject.SetActive(false);
        _skipText.SetActive(false);
        CameraManager.instance.CompletePrevFeedBack();

        Pattern0();
    }

    private void Pattern0()
    {
        StartCoroutine(PatternZeroCoroutine());
    }

    private IEnumerator PatternZeroCoroutine()
    {
        for (int i = 0; i < _pattrnZeroTrms.Length; i++)
        {
            transform.position = _pattrnZeroTrms[i].position;
            _pisaBulletManager.SpawnBulletLookTarget(PisaBulletType.SMALL, _target, transform.position, 7, 0.04f, 7f);
            yield return new WaitForSeconds(0.04f * 7f + 0.3f);
        }

        for (int i = 0; i < _pattrnZeroTrms.Length; i++)
        {
            transform.position = _pattrnZeroTrms[i].position;
            _pisaBulletManager.SpawnBulletByCircle(PisaBulletType.SMALL, transform.position, 36, 7f);
            yield return new WaitForSeconds(0.8f);
        }

        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();
        _seq.Append(transform.DOMove(_pattrnZeroTrmsTwo[0].position, 1f));
        _seq.AppendCallback(() =>
        {
            _pisaBulletManager.SpawnBulletLookTarget(PisaBulletType.SMALL, _target, transform, 60, 0.05f, 6f);
        });
        for (int i = 1; i < _pattrnZeroTrmsTwo.Length; i++)
        {
            _seq.Append(transform.DOMove(_pattrnZeroTrmsTwo[i].position, 1.5f));
        }
        _seq.AppendInterval(2f);
        _seq.AppendCallback(() =>
        {
            _pisaBulletManager.SpawnBulletLookTarget(PisaBulletType.SMALL, _target, transform, 60, 0.05f, 6f);
        });
        for (int i = _pattrnZeroTrmsTwo.Length - 1; i >= 0; i--)
        {
            Vector3 pos = _pattrnZeroTrmsTwo[i].position;
            pos.x *= -1f;
            _seq.Append(transform.DOMove(pos, 1.5f));
        }
        _seq.AppendInterval(0.75f);

        for (int i = 0; i < 4; i++)
        {
            _seq.Append(transform.DOMoveY(1.34f, 0.5f));
            _seq.AppendCallback(() =>
            {
                _pisaBulletManager.SpawnBulletByCircle(PisaBulletType.SMALL, transform.position, 36, 3f);
            });
            _seq.Append(transform.DOMoveY(-1.34f, 0.25f));
            _seq.AppendCallback(() =>
            {
                CameraManager.instance.CameraShake(10f, 40f, 0.2f);
                AudioPoolable au = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
                au.Play(_groundPoundClip, 0.8f);
            });
        }
        for (int i = 0; i < 4; i++)
        {
            _seq.Append(transform.DOMoveY(2f, 0.5f));
            _seq.AppendCallback(() =>
            {
                _pisaBulletManager.SpawnBulletByCircle(PisaBulletType.SMALL, transform.position, 36, 3f);
            });
            _seq.Append(transform.DOMoveY(-1.34f, 0.25f));
            _seq.AppendCallback(() =>
            {
                CameraManager.instance.CameraShake(20f, 40f, 0.2f);
                AudioPoolable au = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
                au.Play(_groundPoundClip, 0.8f);
            });
        }
        _seq.AppendInterval(2f);
        _seq.AppendCallback(() =>
        {
            GameObject dalgo = Instantiate(_dalgonaGift, _bossObjectTrm);
            dalgo.transform.position = Vector3.up * 3f;
        });
        _seq.Append(transform.DOMove(_originPos, 0.5f));
        _seq.AppendCallback(() =>
        {
            StartCoroutine(SpawnMovingFire());
        });
        _seq.AppendInterval(10.5f);
        _seq.AppendCallback(() =>
        {
            Pattern0();
        });
    }

    private IEnumerator SpawnMovingFire()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject movingFire = Instantiate(_movingFireObj, _bossObjectTrm);
            movingFire.transform.position = Vector3.right * -8.6f + Vector3.up * -3f;
            yield return new WaitForSeconds(2f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (_isSkipable == false) return;

            _isSkipable = false;
            transform.position = Vector3.zero;
            BossRoutine();
        }
    }

    public override void ResetBoss()
    {
        if (_seq != null)
            _seq.Kill();
        StopAllCoroutines();
        transform.position = _originPos;
        CameraManager.instance.CompletePrevFeedBack();
        _isSkipable = true;
    }

    public void DieReset()
    {
        _col.enabled = false;
        if (_seq != null)
            _seq.Kill();
        StopAllCoroutines();
        CameraManager.instance.CompletePrevFeedBack();
        transform.position = new Vector3(0f, 1.34f, 0f);
    }

    public void NextPhase()
    {
        if (_seq != null)
            _seq.Kill();

        _spriteRenderer.enabled = true;
        _seq = DOTween.Sequence();
        _seq.Append(transform.DOMoveY(_originPos.y, 0.5f));
        _seq.AppendCallback(() =>
        {
            _movingObj.SetActive(true);
            if (_seq != null)
                _seq.Kill();
        });
    }
}
