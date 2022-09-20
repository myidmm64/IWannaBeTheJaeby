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
    private RacheSprite _racheSprite = null;

    [SerializeField]
    private GameObject _rightFireWalls = null;
    [SerializeField]
    private GameObject _leftFireWalls = null;
    [SerializeField]
    private AudioClip _bulletShotClip = null;
    [SerializeField]
    private Sprite _bulletSprite = null;

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
        }

        _leftFireWalls.transform.position = Vector3.right * -12.5f;
        _rightFireWalls.transform.position = Vector3.right * 12.5f;

        BossRoutine();
    }

    private void BossRoutine()
    {
        Pattern0();
    }

    private void Pattern0()
    {
        SpawnBreaths(_startSpawnBreathPositions, Pattern1);
    }

    private void Pattern1()
    {
        Debug.Log("¾Æ¾Æ¾Æ¾Ó");
        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();
        _seq.Append(_leftFireWalls.transform.DOMoveX(-7.6f, 1f));
        _seq.Join(_rightFireWalls.transform.DOMoveX(7.6f, 1f));
        _seq.AppendCallback(() =>
        {

        });
    }

    public override void ResetBoss()
    {
        StopAllCoroutines();
        if (_seq != null)
            _seq.Kill();
        _animator.SetBool(_attackHash, false);
        DetachBreath();
        _racheSprite.FlipReset();
        BreathSize = 1f;
        transform.position = _originPos;
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

    private void SpawnBulletByCircle(int count)
    {
        CameraManager.instance.CameraShake(20f, 4f, 0.2f);
        AudioPoolable au = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
        au.Play(_bulletShotClip, 0.8f);
        for (int i = 0; i <= 360 / count; i++)
        {
            Barrage s = PoolManager.Instance.Pop("Barrage") as Barrage;
            s.transform.SetParent(_bossObjectTrm);
            Quaternion rot = Quaternion.AngleAxis(i * 10f, Vector3.forward);
            s.transform.SetPositionAndRotation(transform.position, rot);
            s.SetBarrage(5f, new Vector2(0.4f, 0.69f), Vector2.zero, _bulletSprite);
            s.transform.localScale = Vector3.one * 0.7f;
        }
    }

    private void DetachBreath()
    {
        _racheSprite.DetachBreath();
    }

    public void AchievementSet()
    {
        _achievements.Popup("¸¶¿ÕÀ» ¹«Âî¸£´Ù", "ÇØÄ¡¿ü³ª..?", 5);
    }

    public void DieReset()
    {
        ResetBoss();
    }

}
