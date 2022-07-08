using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDamage : AgentDamage
{
    [field: SerializeField]
    private UnityEvent OnDie = null;

    protected override void Die()
    {
        Debug.Log("ав╬З╬Н©Д");
        
        OnDie?.Invoke();
        transform.root.gameObject.SetActive(false);
    }
}
