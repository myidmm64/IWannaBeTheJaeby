using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class BossSoldier : Boss
{
    [SerializeField]
    private Achievements _achievements = null;

    [SerializeField]
    private Transform _enemysTrm = null;
    [SerializeField]
    private GameObject _firstGroundTile = null;
    private SoldierUIManager _soldierUIManager = null;
    private Vector3 _originPos = Vector3.zero;

    [SerializeField]
    private GameObject _jumpInteractionObject = null;
    [SerializeField]
    private AudioClip _jumpClip = null;
    private PlayerMovement _playerMovement = null;
    [SerializeField]
    private Sprite _bulletSprite = null;
    [SerializeField]
    private Sprite[] _poopSprites = null;
    [SerializeField]
    private AudioClip _shootClip = null;
    [SerializeField]
    private AudioClip _powerShootClip = null;

    private Sequence _seq = null;

    private Coroutine _jumpCoroutine = null;
    private Coroutine _playerStopCoroutine = null;

    private SpriteRenderer _spriteRenderer = null;

    private void Start()
    {
        _originPos = transform.position;
        _playerMovement = Save.Instance.playerMovemant;
    }

    private void OnEnable()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = transform.Find("AgentSprite").GetComponent<SpriteRenderer>();
        if( _soldierUIManager == null)
            _soldierUIManager = GetComponent<SoldierUIManager>();
        BossRoutine();
        _firstGroundTile.SetActive(true);
        _spriteRenderer.enabled = true;
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
        _soldierUIManager.SetText("플레이어가", "1초마다", "뛰어오른다", 0f, 1f, ()=>
        {
            _jumpInteractionObject.GetComponent<MoveInteraction>().TargetsReset();
            _jumpInteractionObject.GetComponent<Collider2D>().enabled = true;
            _jumpInteractionObject.SetActive(true);
            _jumpCoroutine = StartCoroutine(PlayerJumpCoroutine());
        });
        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();
        _seq.AppendInterval(9f);
        _seq.AppendCallback(() => Pattern2());
    }

    private void Pattern2()
    {
        if (_seq != null)
            _seq.Kill();
        _soldierUIManager.SetText("보스가", "탄막을", "뿌린다", 0.5f, 1f, () =>
        {
            StopCoroutine(_jumpCoroutine);
            StartCoroutine(ThrowBullet());
        });
    }

    private void Pattern3()
    {
        _soldierUIManager.SetText("플레이어가", "1초마다", "멈춘다", 1f, 1f, () =>
        {
            _playerStopCoroutine = StartCoroutine(PlayerStopCoroutine());
            StartCoroutine(SpawnPoop());
        });
    }

    private void Pattern4()
    {
        _soldierUIManager.SetText("보스가", "2초간", "빨라진다", 1f, 1f, () =>
        {
            if (_seq != null)
                _seq.Kill();
            Vector3 returnPos = transform.position - Vector3.up * 3.2f;
            _seq = DOTween.Sequence();
            _seq.Append(transform.DOScale(1.5f, 0.25f));
            _seq.Append(transform.DOMove(new Vector3(-8f, -3f), 0.5f));
            _seq.Append(transform.DOMove(new Vector3(8f, -3f), 0.5f));
            _seq.AppendInterval(0.25f);
            _seq.Append(transform.DOMove(new Vector3(-8f, -3f), 0.5f));
            _seq.AppendInterval(0.25f);
            _seq.Append(transform.DOMove(returnPos, 0.5f));
            _seq.AppendCallback(() =>
            {
                Pattern5(returnPos);
            });
        });
    }

    private void Pattern5(Vector3 returnPos)
    {
        _soldierUIManager.SetText("보스가", "엄청나게", "커진다", 0.5f, 1f, () =>
        {
            if (_seq != null)
                _seq.Kill();
            _seq = DOTween.Sequence();
            _seq.Append(transform.DOScale(4f, 0.5f));
            _seq.AppendInterval(0.3f);
            _seq.Append(transform.DOScale(7f, 0.5f));
            _seq.AppendInterval(0.3f);
            _seq.Append(transform.DOScale(10f, 0.5f));
            _seq.AppendInterval(2f);
            _seq.Append(transform.DOMoveY(3f, 1f));
            _seq.Join(transform.DOScale(1f, 0.5f));
            _seq.AppendCallback(() =>
            {
                Pattern1();
            });
        });
    }

    private IEnumerator ThrowBullet()
    {
        Vector3 origin = transform.position;
        origin.x = -8f;
        transform.position = origin;
        SpawnBullet();
        yield return new WaitForSeconds(0.5f);
        origin.x = 8f;
        transform.position = origin;
        SpawnBullet();
        yield return new WaitForSeconds(0.5f);
        origin.x = 0f;
        transform.position = origin;
        SpawnBullet();
        yield return new WaitForSeconds(2f);
        Pattern3();
    }

    private IEnumerator SpawnPoop()
    {
        CameraManager.instance.CameraShake(5f, 20f, 12f, true);
        Vector3 pos = new Vector3(0f, 5f);
        for(int i = 0; i < 200; i++)
        {
            Barrage s = PoolManager.Instance.Pop("Barrage") as Barrage;
            s.transform.SetParent(_bossObjectTrm);
            pos.x = Random.Range(-8f, 8f);
            s.transform.SetPositionAndRotation(pos, Quaternion.AngleAxis(180f, Vector3.forward));
            s.SetBarrage(7.5f, new Vector2(0.34f, 0.34f), Vector2.zero, _poopSprites[Random.Range(0, _poopSprites.Length)]);
            s.transform.localScale = Vector3.one * 1f;
            AudioPoolable au = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
            au.Play(_shootClip, 0.6f);
            yield return new WaitForSeconds(0.05f);
        }
        StopCoroutine(_playerStopCoroutine);
        _playerMovement.IsStop = false;
        Pattern4();
    }

    private void SpawnBullet()
    {
        CameraManager.instance.CameraShake(20f, 4f, 0.2f);
        AudioPoolable au = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
        au.Play(_powerShootClip, 0.8f);
        for (int i = 0; i <= 36; i++)
        {
            Barrage s = PoolManager.Instance.Pop("Barrage") as Barrage;
            s.transform.SetParent(_bossObjectTrm);
            Quaternion rot = Quaternion.AngleAxis(i * 10f, Vector3.forward);
            s.transform.SetPositionAndRotation(transform.position, rot);
            s.SetBarrage(5f, new Vector2(0.4f, 0.69f), Vector2.zero, _bulletSprite);
            s.transform.localScale = Vector3.one * 0.7f;
        }
    }

    private IEnumerator PlayerJumpCoroutine()
    {
        PlayerNormalJump playerJump = _playerMovement.GetComponent<PlayerNormalJump>();
        while (true)
        {
            playerJump.ForceJump(20, Vector3.up);
            CameraManager.instance.CameraShake(20f, 4f, 0.2f);
            AudioPoolable au = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
            au.PlayRandomness(_jumpClip);
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator PlayerStopCoroutine()
    {
        Rigidbody2D playerRigid = _playerMovement.GetComponent<Rigidbody2D>();
        while (true)
        {
            _playerMovement.IsStop = true;
            playerRigid.velocity = Vector2.zero;

            yield return new WaitForSeconds(0.2f);
            _playerMovement.IsStop = false;
            yield return new WaitForSeconds(0.8f);
        }
    }

    public override void ResetBoss()
    {
        StopAllCoroutines();
        if (_seq != null)
            _seq.Kill();
        transform.position = _originPos;
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;
        _jumpInteractionObject.SetActive(false);
        _playerMovement.IsStop = false;
    }

    public void DieReset()
    {
        ResetBoss();
        transform.position = new Vector3(_originPos.x, 3f);
    }
    public void AchievementSet()
    {
        _achievements.Popup("단어의 1인자", "'플레이어가','기쁨에','춤춘다'", 3);
    }
}
