using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetInteraction : Interaction
{
    [field: SerializeField]
    private UnityEvent<Collider2D> OnTarget = null;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(collision.CompareTag("Player"))
        {
            OnTarget?.Invoke(collision);
        }
    }

    public override void DoEnterInteraction()
    {
    }

    public override void DoExitInteraction()
    {
    }

    public override void DoStayInteraction()
    {
    }
}
