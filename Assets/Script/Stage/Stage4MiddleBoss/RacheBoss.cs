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
        StartCoroutine(StartAnimationCoroutine());
    }
    private IEnumerator StartAnimationCoroutine()
    {
        _animator.SetBool(_attackHash, true);
        yield return new WaitForSeconds(2f);
        _animator.SetBool(_attackHash, false);
    }

    public override void ResetBoss()
    {
        StopAllCoroutines();
        _animator.SetBool(_attackHash, false);
    }

    public void AchievementSet()
    {
        _achievements.Popup("¸¶¿ÕÀ» ¹«Âî¸£´Ù", "ÇØÄ¡¿ü³ª..?", 5);
    }
}
