using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class RacheBoss : Boss
{
    [SerializeField]
    private Achievements _achievements = null;
    private Animator _animator = null;
    private readonly int _attackHash = Animator.StringToHash("Attack");
    private readonly int _bigAttackHash = Animator.StringToHash("BigAttack");
    private RacheSprite _racheSprite = null;

    [SerializeField]
    private GameObject _rightFireWalls = null;
    [SerializeField]
    private GameObject _leftFireWalls = null;
    [SerializeField]
    private AudioClip _bulletShotClip = null;
    [SerializeField]
    private AudioClip _breathClip = null;
    [SerializeField]
    private Sprite _bulletSprite = null;
    [SerializeField]
    private Transform[] _firePositions = null;
    [SerializeField]
    private GameObject _fireObject = null;
    [SerializeField]
    private GameObject _moveingFireObject = null;
    [SerializeField]
    private Transform _moveingFireTrm = null;

    [SerializeField]
    private GameObject _bonusTileObject = null;
    private Vector3 _bonusTileObjectOriginPos = Vector3.zero;

    private bool _isFireBreathing = false;
    private Transform _targetTrm = null;

    [SerializeField]
    private List<SpawnBreathPos> _startSpawnBreathPositions = new List<SpawnBreathPos>();
    private Vector3 _originPos = Vector3.zero;

    private Sequence _seq = null;

    private float BreathSize
    {
        get
        {
            if (_racheSprite == null) return 1f;
            return _racheSprite.BreathSize;
        }
        set
        {
            if (_racheSprite == null) return;
            _racheSprite.BreathSize = value;
        }
    }

    [System.Serializable]
    public struct SpawnBreathPos
    {
        public Transform trm;
        public bool flip;
        public float moveDuration;
        public float enterDuration;
        public float duration;
        public float breathSize;
    }

    private void OnEnable()
    {
        if (_animator == null)
        {
            _animator = transform.Find("AgentSprite").GetComponent<Animator>();
            _racheSprite = _animator.GetComponent<RacheSprite>();
            _originPos = transform.position;
            _bonusTileObjectOriginPos = _bonusTileObject.transform.position;
            _targetTrm = Save.Instance.playerMovemant.transform;
        }
        _racheSprite.GetComponent<SpriteRenderer>().enabled = true;
        _racheSprite.GetComponent<Collider2D>().enabled = true;
        _leftFireWalls.transform.position = Vector3.right * -7.6f;
        _rightFireWalls.transform.position = Vector3.right * 7.6f;

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
        _seq.Append(_leftFireWalls.transform.DOMoveX(-12.5f, 1f));
        _seq.Join(_rightFireWalls.transform.DOMoveX(12.5f, 1f));
        _seq.AppendCallback(() =>
        {
            SpawnBreaths(_startSpawnBreathPositions, Pattern1);
        });
    }

    private void Pattern1()
    {
        Debug.Log("¾Æ¾Æ¾Æ¾Ó");
        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();
        _seq.Append(_leftFireWalls.transform.DOMoveX(-7.6f, 1f));
        _seq.Join(_rightFireWalls.transform.DOMoveX(7.6f, 1f));
        _seq.Join(transform.DOMove(_originPos, 0.8f));
        _seq.AppendCallback(() =>
        {
            StartCoroutine(Pattern1BulletCoroutine());
        });
    }

    private void Pattern2()
    {
        StartCoroutine(SpawnFireCoroutine());
    }

    private void Pattern3()
    {
        StartCoroutine(SpawnMovingFireObejcts());
    }

    private void Pattern4()
    {
        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();
        _seq.Append(_bonusTileObject.transform.DOMoveY(0f, 0.5f));
        _seq.Append(transform.DOMove(new Vector3(-7f, transform.position.y, transform.position.z), 0.5f));
        _seq.AppendCallback(() =>
        {
            _isFireBreathing = true;
            StartCoroutine(BigAttackCoroutine());
        });
    }

    private IEnumerator BigAttackCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _animator.SetBool(_bigAttackHash, true);
        _isFireBreathing = false;
        yield return new WaitForSeconds(1f);
        _animator.SetBool(_bigAttackHash, false);
        _isFireBreathing = true;
        yield return new WaitForSeconds(0.8f);
        _animator.SetBool(_bigAttackHash, true);
        _isFireBreathing = false;
        yield return new WaitForSeconds(1f);
        _animator.SetBool(_bigAttackHash, false);
        _isFireBreathing = true;
        yield return new WaitForSeconds(0.8f);
        _animator.SetBool(_bigAttackHash, true);
        _isFireBreathing = false;
        yield return new WaitForSeconds(1f);
        _animator.SetBool(_bigAttackHash, false);
        _isFireBreathing = true;
        yield return new WaitForSeconds(0.8f);
        _animator.SetBool(_bigAttackHash, true);
        _isFireBreathing = false;
        yield return new WaitForSeconds(1f);
        _animator.SetBool(_bigAttackHash, false);
        _isFireBreathing = true;
        yield return new WaitForSeconds(0.8f);
        _animator.SetBool(_bigAttackHash, true);
        _isFireBreathing = false;
        yield return new WaitForSeconds(1f);
        _animator.SetBool(_bigAttackHash, false);
        yield return new WaitForSeconds(0.8f);

        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();
        _seq.Append(_bonusTileObject.transform.DOMove(_bonusTileObjectOriginPos * -1f, 0.25f).OnComplete(()=>
        {
            _bonusTileObject.transform.position = _bonusTileObjectOriginPos;
        }));
        _seq.Append(transform.DOMove(_originPos, 0.5f));
        _seq.AppendCallback(() =>
        {
            Pattern0();
        });
    }

    private void Update()
    {
        if (_isFireBreathing == false) return;
        Vector3 cur = transform.position;
        cur.y = _targetTrm.position.y;
        transform.position = cur;
    }

    private IEnumerator SpawnMovingFireObejcts()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            GameObject obj = Instantiate(_moveingFireObject, _bossObjectTrm);
            obj.transform.position = _moveingFireTrm.position + Vector3.right;
        }
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            GameObject obj = Instantiate(_moveingFireObject, _bossObjectTrm);
            obj.transform.position = _moveingFireTrm.position;
        }
        yield return new WaitForSeconds(3.5f);
        Pattern4();
    }

    private IEnumerator SpawnFireCoroutine()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < _firePositions.Length; i++)
        {
            GameObject fire = Instantiate(_fireObject, _bossObjectTrm);
            fire.transform.SetPositionAndRotation(_firePositions[i].position, Quaternion.Euler(new Vector3(0f, 0f, -90f)));
            fire.GetComponent<BigFire>().Init(0.3f);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1.1f);
        for (int i = _firePositions.Length - 1; i >= 0; i--)
        {
            GameObject fire = Instantiate(_fireObject, _bossObjectTrm);
            fire.transform.SetPositionAndRotation(_firePositions[i].position + Vector3.right * 1.5f, Quaternion.Euler(new Vector3(0f, 0f, -90f)));
            fire.GetComponent<BigFire>().Init(0.3f);
            yield return new WaitForSeconds(0.1f);
        }
        Pattern3();
    }

    private IEnumerator Pattern1BulletCoroutine()
    {
        SpawnBulletByCircle(transform.position, 36, 4f);
        SpawnBulletByCircle(transform.position + new Vector3(-3f, 3f), 36, 4f);
        SpawnBulletByCircle(transform.position - new Vector3(-3f, -3f), 36, 4f);
        yield return new WaitForSeconds(1.5f);
        SpawnBulletByCircle(transform.position, 36, 3.5f);
        yield return new WaitForSeconds(1f);
        SpawnBulletByCircle(transform.position + new Vector3(1.5f, 2f), 36, 4f);
        SpawnBulletByCircle(transform.position + new Vector3(-1.5f, 2f), 36, 4f);
        yield return new WaitForSeconds(1f);
        SpawnBulletByCircle(transform.position, 36, 3.5f);
        Pattern2();
    }

    public override void ResetBoss()
    {
        StopAllCoroutines();
        if (_seq != null)
            _seq.Kill();
        _animator.SetBool(_attackHash, false);
        _animator.SetBool(_bigAttackHash, false);
        DetachBreath();
        _racheSprite.FlipReset();
        BreathSize = 1f;
        transform.position = _originPos;
        _isFireBreathing = false;
        _bonusTileObject.transform.position = _bonusTileObjectOriginPos;
    }

    private void SpawnBreaths(List<SpawnBreathPos> breathPositions, Action Callback)
    {
        _seq = DOTween.Sequence();
        for (int i = 0; i < breathPositions.Count; i++)
        {
            if (breathPositions[i].trm == null) continue;
            SpawnBreathPos target = breathPositions[i];
            _seq.Append(transform.DOMove(target.trm.position, target.moveDuration));
            _seq.AppendCallback(() =>
            {
                _racheSprite.FlipSprite(target.flip);
            });
            _seq.AppendInterval(target.enterDuration);
            _seq.AppendCallback(() =>
            {
                BreathSize = target.breathSize;
                _animator.SetBool(_attackHash, true);
                StartCoroutine( ShakeCoroutine(target.breathSize, target.duration));
            });
            _seq.AppendInterval(target.duration);
            _seq.AppendCallback(() =>
            {
                _animator.SetBool(_attackHash, false);
            });
        }
        _seq.AppendCallback(() =>
        {
            Callback?.Invoke();
        });
    }

    private IEnumerator ShakeCoroutine(float time, float duration)
    {
        yield return new WaitForSeconds(0.3f);
        AudioPoolable au = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
        au.Play(_breathClip);
        CameraManager.instance.CameraShake(2f * time, 20, duration);
    }

    private void SpawnBulletByCircle(Vector3 pos, int count, float speed)
    {
        CameraManager.instance.CameraShake(10f, 4f, 0.2f);
        AudioPoolable au = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
        au.Play(_bulletShotClip, 0.6f);
        for (int i = 0; i <= count; i++)
        {
            Barrage s = PoolManager.Instance.Pop("Barrage") as Barrage;
            s.transform.SetParent(_bossObjectTrm);
            Quaternion rot = Quaternion.AngleAxis(i * 10f, Vector3.forward);
            s.transform.SetPositionAndRotation(pos, rot);
            s.SetBarrage(speed, new Vector2(0.49f, 0.49f), Vector2.zero, _bulletSprite);
            s.transform.localScale = Vector3.one * 0.5f;
        }
    }

    private void DetachBreath()
    {
        _racheSprite.DetachBreath();
    }

    public void AchievementSet()
    {
        _achievements.Popup("Defeating the Demon", "Did you finish it..?", 5);
    }

    public void DieReset()
    {
        ResetBoss();
        _racheSprite.GetComponent<Collider2D>().enabled = false;
    }

}
