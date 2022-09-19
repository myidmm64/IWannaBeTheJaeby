using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RacheBoss : Boss
{
    [SerializeField]
    private Achievements _achievements = null;
    private Animator _animator = null;
    private readonly int _attackHash = Animator.StringToHash("Attack");
    private RacheSprite _racheSprite = null;
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
        BossRoutine();
    }

    private void BossRoutine()
    {
        Pattern0();
    }

    private void Pattern0()
    {
        SpawnBreaths(_startSpawnBreathPositions);
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

    private void SpawnBreaths(List<SpawnBreathPos> breathPositions)
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
    }

    private void DetachBreath()
    {
        _racheSprite.DetachBreath();
    }

    public void AchievementSet()
    {
        _achievements.Popup("¸¶¿ÕÀ» ¹«Âî¸£´Ù", "ÇØÄ¡¿ü³ª..?", 5);
    }

}
