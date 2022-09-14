using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : AgentDamage
{
    [SerializeField]
    private AudioClip _damageClip = null;
    private Collider2D _col = null;
    private Collider2D _parentsCol = null;

    protected override void Awake()
    {
        base.Awake();
        _col = GetComponent<Collider2D>();
        _parentsCol = transform.parent.GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        _hp = _maxHP;
        _col.enabled = true;
        _spriteRenderer.enabled = true;
        _parentsCol.enabled = true;
    }
    public override void Die()
    {
        WaitAndPushPoolable a = PoolManager.Instance.Pop("EnemyDieParticle") as WaitAndPushPoolable;
        a.transform.position = transform.position;
        a.GetComponent<ParticleSystem>().Play();
        a.Push(2f);

        _col.enabled = false;
        _spriteRenderer.enabled = false;
        _parentsCol.enabled = false;
    }
    public override void Damaged()
    {
        base.Damaged();
        AudioPoolable au = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
        au.PlayRandomness(_damageClip);
    }
}
