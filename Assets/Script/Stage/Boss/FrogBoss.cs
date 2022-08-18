using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FrogBoss : Boss
{
    [SerializeField]
    private Transform _startPos = null;
    [SerializeField]
    private Transform _mainPos = null;

    [SerializeField]
    private Transform[] _jumpPoss = null;

    private Sequence _seq = null;
    private Rigidbody2D _rigid = null;
    private SpriteRenderer _spriteRenderer = null;

    private Animator _baseAnimator = null;

    [Header("스폰하는 녀석")]
    [SerializeField]
    private Transform _bassSpawnTrm = null;
    [SerializeField]
    private GameObject _torchObj = null;
    [SerializeField]
    private GameObject _bucketObj = null;
    [SerializeField]
    private GameObject _flyObj = null;
    [SerializeField]
    private Transform[] _jumpEffectPos = null;

    [Header("애니메이터 관련")]
    [SerializeField]
    private RuntimeAnimatorController _baseFrogController = null;
    [SerializeField]
    private RuntimeAnimatorController _fireFrogController = null;
    [SerializeField]
    private RuntimeAnimatorController _waterFrogController = null;
    [SerializeField]
    private RuntimeAnimatorController _flyFrogController = null;

    private bool _isJumping = false;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = transform.Find("AgentSprite").GetComponent<SpriteRenderer>();
        _baseAnimator = _spriteRenderer.GetComponent<Animator>();
    }

    private enum NextPattern
    {
        FIRE,
        WATER,
        FLY,
        SIZE
    }

    public void StartBoss()
    {
        BossRoutine();
    }

    private void BossRoutine()
    {
        Pattern0();
    }

    private void Pattern0()
    {
        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();

        _seq.Append(transform.DOMove(_mainPos.position, 1.5f).SetEase(Ease.Linear));
        _seq.AppendCallback(() =>
        {
            _baseAnimator.SetTrigger("Tounge");
        });
        _seq.AppendInterval(0.5f);
        _seq.AppendCallback(() =>
        {
            _torchObj.SetActive(false);
            _baseAnimator.runtimeAnimatorController = _fireFrogController;
        });
        _seq.AppendInterval(1f);
        _seq.AppendCallback(() =>
        {
            SpawnFireball();
            _baseAnimator.runtimeAnimatorController = _baseFrogController;
            if (_seq != null)
                _seq.Kill();
            Pattern1();
        });
    }

    private void Pattern1()
    {
        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();

        Jump(_jumpPoss[0].position);
        Jump(_jumpPoss[1].position);
        Jump(_jumpPoss[2].position);
        Jump(_jumpPoss[3].position);
        _seq.AppendCallback(() =>
        {
            _spriteRenderer.flipX = true;
        });
        Jump(_jumpPoss[2].position);
        Jump(_jumpPoss[1].position);
        Jump(_jumpPoss[0].position);
        Jump(new Vector3(_mainPos.position.x, _jumpPoss[0].position.y, 0f));
        _seq.AppendCallback(() =>
        {
            _spriteRenderer.flipX = false;

            if (_seq != null)
                _seq.Kill();
            Pattern2();
        });
    }

    private void Pattern2()
    {
        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();


        GameObject target = null;
        NextPattern nextPattern = NextPattern.FIRE;

        switch ((NextPattern)Random.Range(0, (int)NextPattern.SIZE))
        {
            case NextPattern.FIRE:
                nextPattern = NextPattern.FIRE;
                target = _torchObj;
                _torchObj.SetActive(true);
                break;
            case NextPattern.WATER:
                nextPattern = NextPattern.WATER;
                target = _bucketObj;
                _bucketObj.SetActive(true);
                break;
            case NextPattern.FLY:
                nextPattern = NextPattern.FLY;
                target = _flyObj;
                _flyObj.SetActive(true);
                break;
            default:
                break;
        }
        _seq.AppendInterval(1f);
        _seq.AppendCallback(() => { _baseAnimator.SetTrigger("Tounge"); });
        _seq.AppendInterval(0.5f);
        _seq.AppendCallback(() => 
        { 
            target.SetActive(false);
            switch(nextPattern)
            {
                case NextPattern.FIRE:
                    _baseAnimator.runtimeAnimatorController = _fireFrogController;
                    break;
                case NextPattern.WATER:
                    _baseAnimator.runtimeAnimatorController = _baseFrogController;
                    _spriteRenderer.color = Color.blue;
                    break;
                case NextPattern.FLY:
                    _baseAnimator.runtimeAnimatorController = _flyFrogController;
                    break;
            }

            if (_seq != null)
                _seq.Kill();
            Pattern3(nextPattern);
        });
    }

    private void Pattern3(NextPattern nextPattern)
    {
        switch(nextPattern)
        {
            case NextPattern.FIRE:
                FireballSpawnPattern();
                break;
            case NextPattern.WATER:
                FireballSpawnPattern();
                break;
            case NextPattern.FLY:
                FireballSpawnPattern();
                break;
        }
    }

    private void FireballSpawnPattern()
    {
        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();

        int randomCount = Random.Range(1, 4);

        for(int i = 0; i<randomCount; i++)
        {
            if(i == 0)
                _seq.AppendInterval(2f);
            else
                _seq.AppendInterval(2.5f);

            _seq.AppendCallback(() =>
            {
                SpawnFireball();
            });
        }
        _seq.AppendCallback(() =>
        {
            _baseAnimator.runtimeAnimatorController = _baseFrogController;
            _spriteRenderer.color = Color.white;
            if (_seq != null)
                _seq.Kill();
            Pattern1(); 
        });
    }

    private void Jump(Vector3 pos)
    {
        _seq.AppendInterval(0.5f);
        _seq.Append(transform.DOMove(pos, 0.5f));
        _seq.AppendInterval(0.3f);
        _seq.Append(transform.DOMoveY(_mainPos.position.y, 0.2f));
        _seq.AppendCallback(() =>
        {
            CameraManager.instance.CameraShake(4f, 10f, 0.2f);
            SpawnJumpPressEffect();
        });
    }

    private void SpawnFireball()
    {
        CameraManager.instance.CameraShake(2f, 10f, 0.5f);
        BulletMove fireball = PoolManager.Instance.Pop("FireBall") as BulletMove;
        fireball.transform.SetParent(_bassSpawnTrm);
        fireball.transform.position = transform.position + Vector3.right * 2f;
    }

    private void SpawnJumpPressEffect()
    {
        for(int i = 0; i<_jumpEffectPos.Length; i++)
        {
            ShockWave shock = PoolManager.Instance.Pop("ShockWave") as ShockWave;
            shock.transform.position = _jumpEffectPos[i].position;
            shock.StartAnimation();
        }
    }

    public override void ResetBoss()
    {
        if (_seq != null)
            _seq.Kill();
        StopAllCoroutines();
        transform.position = _startPos.position;
        _torchObj.SetActive(true);
        _bucketObj.SetActive(false);
        _flyObj.SetActive(false);
        _spriteRenderer.color = Color.white;
        if (_spriteRenderer != null)
            _spriteRenderer.flipX = false;
        if (_baseAnimator != null)
            _baseAnimator.runtimeAnimatorController = _baseFrogController;

        PoolableMono[] bossPoolable = _bassSpawnTrm.GetComponentsInChildren<PoolableMono>();
        for (int i = 0; i < bossPoolable.Length; i++)
        {
            PoolManager.Instance.Push(bossPoolable[i]);
        }
    }
}
