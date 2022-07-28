using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : AgentDamage
{
    private void OnEnable()
    {
        _hp = _maxHP;
    }
    public override void Die()
    {
    }
    protected override void Damaged()
    {
        base.Damaged();
    }
}
