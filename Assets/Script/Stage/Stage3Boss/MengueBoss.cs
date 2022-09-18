using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

public class MengueBoss : Boss
{
    [SerializeField]
    private Achievements _achievements = null;
    private SpriteRenderer _spriteRenderer = null;
    private Collider2D _col = null;
    private BossDamaged _damaged = null;

    [SerializeField]
    private GameObject _sword = null;
    private Animator _swordAnimator = null;
    [SerializeField]
    private Transform[] _swordTrms = null;
    [SerializeField]
    private Transform[] _bossTrms = null;
    [SerializeField]
    private Transform[] _jumpTrms = null;
    [SerializeField]
    private GameObject[] _enemys = null;
    [SerializeField]
    private GameObject[] _coins = null;
    [SerializeField]
    private Sprite _bulletSprite = null;
    [SerializeField]
    private GameObject[] _interactionObj = null;

    private Collider2D _swordCol = null;
    private SpriteRenderer _swordSprite = null;

    private Sequence _seq = null;
    private Sequence _animationSeq = null;

    [SerializeField]
    private AudioClip _shootClip = null;

    private bool _isFirst = true;
    private Vector3 _originPos = Vector3.zero;

    private void Awake()
    {
        _col = transform.Find("AgentSprite").GetComponent<Collider2D>();
        _spriteRenderer = _col.GetComponent<SpriteRenderer>();
        _damaged = _col.GetComponent<BossDamaged>();
        _swordAnimator = _sword.GetComponent<Animator>();
    }

    private void Start()
    {
        if (_isFirst)
        {
            _isFirst = false;
            _originPos = transform.position;
        }
        _damaged.ResetHP();
    }


    public void BossStart()
    {
        BossRoutine();
    }

    private void BossRoutine()
    {
        StartCoroutine(Pattern0());
    }

    private IEnumerator Pattern0()
    {
        _swordSprite = _sword.GetComponent<SpriteRenderer>();
        _swordCol = _sword.GetComponent<Collider2D>();
        _swordSprite.enabled = false;
        _swordCol.enabled = false;

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < _swordTrms.Length; i++)
        {
            _swordSprite.enabled = false;
            _swordCol.enabled = false;
            transform.DOMove(_bossTrms[i].position, 0.5f);
            yield return new WaitForSeconds(0.5f);
            if (i == _swordTrms.Length - 1)
                yield return new WaitForSeconds(1.2f);

            _swordSprite.enabled = true;
            _swordCol.enabled = true;

            _sword.transform.position = _swordTrms[i].position;

            _swordAnimator.SetTrigger("Slash" + i);
            CameraManager.instance.CameraShake(2f, 30f, 0.6f);
            yield return new WaitForSeconds(0.7f);
        }

        _swordSprite.enabled = false;
        _swordCol.enabled = false;
        transform.DOMove(_bossTrms[_swordTrms.Length].position, 1f).OnComplete(() => { Pattern1(); });
    }

    private void Pattern1()
    {
        if (_seq != null)
            _seq.Kill();
        transform.DOKill();

        transform.position = new Vector3(_jumpTrms[0].position.x, _bossTrms[_swordTrms.Length].position.y, 0f);
        _seq = DOTween.Sequence();
        _seq.Append(transform.DOMove(_jumpTrms[0].position, 0.5f));
        _seq.Append(transform.DOJump(_jumpTrms[1].position, 13f, 1, 0.7f));
        _seq.AppendCallback(() =>
        {
            for (int i = 0; i < _enemys.Length; i++)
            {
                _enemys[i].SetActive(true);
            }
        });
        _seq.AppendInterval(0.5f);
        _seq.AppendCallback(() =>
        {
            _damaged.IsGodMode = true;
        });
        _seq.Append(transform.DOMove(_bossTrms[_swordTrms.Length].position, 0.5f));
        _seq.AppendCallback(() =>
        {
            _damaged.IsGodMode = false;

            if (_seq != null)
                _seq.Kill();
            Pattern2();
        });
    }

    private void Pattern2()
    {
        if (_seq != null)
            _seq.Kill();

        transform.position = new Vector3(0, 8f, 0f);
        _seq = DOTween.Sequence();
        _seq.Append(transform.DOMove(_originPos, 1f));
        _seq.AppendCallback(() =>
        {
            StartCoroutine(CoinSpawn());
        });
        _seq.AppendInterval(0.5f * _coins.Length + 1f);
        _seq.AppendCallback(() =>
        {
            for (int i = 0; i < _enemys.Length; i++)
            {
                _enemys[i].SetActive(false);
            }
            if (_seq != null)
                _seq.Kill();
            Pattern3();
        });
    }

    private void Pattern3()
    {
        StartCoroutine(SpawnBullet());
    }

    private void Pattern4()
    {
        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();
        _seq.AppendInterval(2f);
        _seq.Append(transform.DOMoveY(-3f, 0.25f).SetEase(Ease.InOutBack));
        _seq.AppendCallback(() =>
        {
            CameraManager.instance.CameraShake(10f, 40f, 0.5f);
        });
        _seq.AppendInterval(2f);
        _seq.Append(transform.DOMove(_originPos, 0.5f));
        _seq.AppendCallback(() =>
        {
            if (_seq != null)
                _seq.Kill();
            Pattern5();
        });
    }

    private void Pattern5()
    {
        StartCoroutine(Pattern5Coroutine());
    }

    private IEnumerator Pattern5Coroutine()
    {
        for (int i = 0; i < _coins.Length; i++)
        {
            _coins[i].SetActive(false);
        }

        for (int i = 0; i<_interactionObj.Length; i++)
        {
            _interactionObj[i].SetActive(true);
            CameraManager.instance.CameraShake(4f, 30f, 0.2f);
            yield return new WaitForSeconds(0.1f);
            _interactionObj[i].SetActive(false);
        }
        yield return new WaitForSeconds(3f);
        BossRoutine();
    }

    private IEnumerator SpawnBullet()
    {
        if (_animationSeq != null)
            _animationSeq.Kill();
        _animationSeq = DOTween.Sequence();
        _animationSeq.Append(transform.DOShakePosition(0.1f, 0.6f)).SetLoops(-1, LoopType.Yoyo);
        CameraManager.instance.CompletePrevFeedBack();
        CameraManager.instance.CameraShake(5f, 30f, 0.05f * 100 + 1f, true);

        for (int i = 0; i < 100; i++)
        {
            Barrage s = PoolManager.Instance.Pop("Barrage") as Barrage;
            s.transform.SetParent(_bossObjectTrm);
            Quaternion rot = Quaternion.AngleAxis(Random.Range(-30f, -150f) - 90f, Vector3.forward);
            s.transform.SetPositionAndRotation(transform.position, rot);
            s.SetBarrage(5f, new Vector2(0.19f, 0.31f), Vector2.zero, _bulletSprite);
            s.transform.localScale = Vector3.one * 1.2f;
            AudioPoolable au = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
            au.Play(_shootClip, 0.6f);
            yield return new WaitForSeconds(0.05f);
        }

        if (_animationSeq != null)
            _animationSeq.Kill();
        Pattern4();
    }

    private IEnumerator CoinSpawn()
    {
        _animationSeq = DOTween.Sequence();
        _animationSeq.Append(transform.DORotate(new Vector3(0f, 0f, -10f), 0.25f));
        _animationSeq.Append(transform.DORotate(new Vector3(0f, 0f, 0f), 0.25f));
        _animationSeq.Append(transform.DORotate(new Vector3(0f, 0f, 10f), 0.25f));
        _animationSeq.Append(transform.DORotate(new Vector3(0f, 0f, 0f), 0.25f)).SetLoops(-1, LoopType.Restart);

        for (int i = 0; i < _coins.Length; i++)
        {
            _coins[i].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void DieReset()
    {
        _col.enabled = false;

        StopAllCoroutines();
        transform.DOKill();

        if (_seq != null)
            _seq.Kill();
        if (_animationSeq != null)
            _animationSeq.Kill();

        if (_originPos != Vector3.zero)
        {
            
            transform.position = _originPos;
            transform.rotation = Quaternion.identity;
            _sword.transform.position = _swordTrms[0].transform.position;
            _sword.transform.rotation = Quaternion.identity;
            _swordSprite.enabled = false;
            _swordCol.enabled = false;
        }

        for (int i = 0; i < _enemys.Length; i++)
        {
            _enemys[i].SetActive(false);
        }
        for (int i = 0; i < _coins.Length; i++)
        {
            _coins[i].SetActive(false);
        }
        for (int i = 0; i < _interactionObj.Length; i++)
        {
            _interactionObj[i].SetActive(false);
        }
    }

    public override void ResetBoss()
    {
        if (_col != null)
        {
            _col.enabled = true;
            _spriteRenderer.enabled = true;
            _damaged.ResetHP();
        }

        StopAllCoroutines();
        transform.DOKill();

        if (_seq != null)
            _seq.Kill();
        if (_animationSeq != null)
            _animationSeq.Kill();

        if (_originPos != Vector3.zero)
        {
            transform.position = _originPos;
            transform.rotation = Quaternion.identity;
            _sword.transform.position = _swordTrms[0].transform.position;
            _sword.transform.rotation = Quaternion.identity;
        }

        for (int i = 0; i < _enemys.Length; i++)
        {
            _enemys[i].SetActive(false);
        }
        for (int i = 0; i < _coins.Length; i++)
        {
            _coins[i].SetActive(false);
        }
        for (int i = 0; i < _interactionObj.Length; i++)
        {
            _interactionObj[i].GetComponent<MoveInteraction>().TargetsReset();
            _interactionObj[i].SetActive(false);
        }
    }
    public void AchievementSet()
    {
        _achievements.Popup("100만원을 모으다", "이걸로 치킨이나 먹을까?", 4);
    }
}
