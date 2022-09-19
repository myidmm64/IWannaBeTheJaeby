using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacheBoss : Boss
{
    [SerializeField]
    private Achievements _achievements = null;
    private Animator _animator = null;
    private readonly int _attackHash = Animator.StringToHash("Attack");
    private RacheSprite _racheSprite = null;
    [SerializeField]
    private List<SpawnBreathPos> _startSpawnBreathPositions = new List<SpawnBreathPos>();

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
        public float duration;
        public float breathSize;
    }

    private void OnEnable()
    {
        if (_animator == null)
        {
            _animator = transform.Find("AgentSprite").GetComponent<Animator>();
            _racheSprite = _animator.GetComponent<RacheSprite>();
        }
        BossRoutine();
    }

    private void BossRoutine()
    {
        Pattern0();
    }

    private void Pattern0()
    {
        StartCoroutine(SpawnBreathsCoroutine(_startSpawnBreathPositions));
    }

    public override void ResetBoss()
    {
        StopAllCoroutines();
        _animator.SetBool(_attackHash, false);
        DetachBreath();
        _racheSprite.FlipReset();
        BreathSize = 1f;
    }

    private IEnumerator SpawnBreathsCoroutine(List<SpawnBreathPos> breathPositions)
    {
        for (int i = 0; i < breathPositions.Count; i++)
        {
            transform.position = breathPositions[i].trm.position;
            _racheSprite.FlipSprite(breathPositions[i].flip);
            BreathSize = breathPositions[i].breathSize;
            _animator.SetBool(_attackHash, true);
            yield return new WaitForSeconds(breathPositions[i].duration);
            _animator.SetBool(_attackHash, false);
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
