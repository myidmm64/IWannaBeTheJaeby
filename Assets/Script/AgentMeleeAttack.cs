using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentMeleeAttack : MonoBehaviour
{
    [field: SerializeField]
    private UnityEvent OnAttack = null;


    public void Attack()
    {
        OnAttack?.Invoke();
    }
}
