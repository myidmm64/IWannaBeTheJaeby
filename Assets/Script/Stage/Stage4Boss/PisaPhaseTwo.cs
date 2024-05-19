using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

public class PisaPhaseTwo : Boss
{
    [SerializeField]
    private Achievements _achievements = null;
    private Sequence _seq = null;
    private Collider2D _col = null;
    private SpriteRenderer _spriteRenderer = null;
    private Vector3 _originPos = Vector3.zero;
    private Transform _target = null;

    [SerializeField]
    private AudioClip _screamClip = null;

    [SerializeField]
    private SmallFrog _smallFrog = null;
    [SerializeField]
    private GameObject _soldierObj = null;
    [SerializeField]
    private GameObject _racheObj = null;
    [SerializeField]
    private GameObject _mengObj = null;

    [SerializeField]
    private GameObject _rightFireWallObj = null;
    [SerializeField]
    private GameObject _leftFireWallObj = null;

    private PisaBulletManager _pisaBulletManager = null;

    private PlayerMovement _playerMovement = null;

    private Coroutine _stopCoroutine = null;

    private void OnEnable()
    {
        if (_seq != null)
            _seq.Kill();
        if (_originPos == Vector3.zero)
        {
            _originPos = transform.position;
            _col = transform.Find("AgentSprite").GetComponent<Collider2D>();
            _spriteRenderer = _col.GetComponent<SpriteRenderer>();
            _target = Save.Instance.playerMovemant.transform;
            _pisaBulletManager = GetComponent<PisaBulletManager>();
            _playerMovement = Save.Instance.playerMovemant;
        }

        _spriteRenderer.enabled = true;
        _col.enabled = true;

        BossRoutine();
    }

    private void BossRoutine()
    {
        _stopCoroutine = StartCoroutine(PlayerStopCoroutine());
        Pattern0();
    }

    private IEnumerator PlayerStopCoroutine()
    {
        _playerMovement.IsStop = true;
        _playerMovement.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        _playerMovement.IsStop = false;
    }

    private void Pattern0()
    {
        _soldierObj.SetActive(false);
        //_racheObj.SetActive(false);
        _mengObj.SetActive(false);
        _rightFireWallObj.SetActive(false);
        _leftFireWallObj.SetActive(false);

        StartCoroutine(Pattern0Coroutiine());
    }

    private void Pattern1()
    {
        _smallFrog.StartFrogJump();
    }

    public void Pattern2()
    {
        if (_seq != null)
            _seq.Kill();

        _seq = DOTween.Sequence();
        _seq.Append(transform.DOMoveY(8.5f, 0.5f));
        _seq.AppendCallback(() =>
        {
            _soldierObj.SetActive(true);
        });
    }

    public void Pattern3()
    {
        _mengObj.SetActive(true);
    }

    public void Pattern4()
    {
        _rightFireWallObj.SetActive(true);
    }

    public void Pattern5()
    {
        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();
        _seq.Append(transform.DOMove(new Vector3(4.36f, -1.02f, 0f), 1f));
        _seq.AppendCallback(() =>
        {
            StartCoroutine(Pattern5Coroutine());
        });
    }

    public void Pattern6()
    {
        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();
        _seq.AppendInterval(2f);
        _seq.Append(transform.DOMove(_originPos, 1f));
        _seq.AppendCallback(() =>
        {
            Pattern0();
        });
    }

    private IEnumerator Pattern5Coroutine()
    {
        _pisaBulletManager.SpawnBulletByCircle(PisaBulletType.MIDDLE, transform.position, 36, 3f);
        yield return new WaitForSeconds(0.25f);
        _pisaBulletManager.SpawnBulletLookTarget(PisaBulletType.MIDDLE, _target, transform.position, 7, 0.01f, 5f);
        yield return new WaitForSeconds(0.4f);
        _pisaBulletManager.SpawnBulletLookTarget(PisaBulletType.MIDDLE, _target, transform.position, 7, 0.01f, 5f);
        yield return new WaitForSeconds(0.4f);
        _pisaBulletManager.SpawnBulletLookTarget(PisaBulletType.MIDDLE, _target, transform.position, 7, 0.01f, 5f);
        yield return new WaitForSeconds(0.4f);
        _pisaBulletManager.SpawnBulletLookTarget(PisaBulletType.MIDDLE, _target, transform.position, 6, 0.02f, 4f);
        yield return new WaitForSeconds(0.4f);
        _pisaBulletManager.SpawnBulletLookTarget(PisaBulletType.MIDDLE, _target, transform.position, 6, 0.03f, 3f);
        yield return new WaitForSeconds(0.2f);
        _pisaBulletManager.SpawnBulletLookTarget(PisaBulletType.MIDDLE, _target, transform.position, 5, 0.04f, 2f);
        yield return new WaitForSeconds(3f);
        AudioPoolable au = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
        au.Play(_screamClip, 1f);
        CameraManager.instance.CameraShake(20f, 40f, 2f);
        yield return new WaitForSeconds(2f);
        _leftFireWallObj.SetActive(true);
    }


    private IEnumerator Pattern0Coroutiine()
    {
        Vector3 targetPos = _target.position;
        _pisaBulletManager.SpawnBulletLookTargetBetween(PisaBulletType.MIDDLE, targetPos, transform.position, 35f, 70, 0.02f, 6f, 0.6f, true);
        yield return new WaitForSeconds(0.5f);
        AudioPoolable au = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
        au.Play(_screamClip, 1f);
        CameraManager.instance.CameraShake(20f, 40f, 2f);
        yield return new WaitForSeconds(0.02f * 70f + 0.5f);
        _pisaBulletManager.SpawnBulletLookTargetBetween(PisaBulletType.MIDDLE, targetPos, transform.position, 35f, 310, 0.02f, 6f, 0.6f);
        yield return new WaitForSeconds(0.02f * 300f + 4f);
        Pattern1();
    }

    public override void ResetBoss()
    {
        if (_seq != null)
            _seq.Kill();
        if (_stopCoroutine != null)
            StopCoroutine(_stopCoroutine);
        StopAllCoroutines();
        _soldierObj.SetActive(false);
        //_racheObj.SetActive(false);
        _mengObj.SetActive(false);
        _rightFireWallObj.SetActive(false);
        _leftFireWallObj.SetActive(false);
        transform.position = _originPos;
        CameraManager.instance.CompletePrevFeedBack();
    }

    public void DieReset()
    {
        _col.enabled = false;
        ResetBoss();
        AudioPoolable au = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
        au.Play(_screamClip, 1f);
    }

    public void AchievementSet()
    {
        _achievements.Popup("I tried my best, but it's going nowhere!", "I saw the end in my hand", 6);
    }

    public void GoEnding()
    {
        StartCoroutine(EndingCoroutine());
    }

    private IEnumerator EndingCoroutine()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(4);
    }
}
