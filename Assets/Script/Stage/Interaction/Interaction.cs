using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interaction : MonoBehaviour
{
    [field: SerializeField]
    private UnityEvent OnInteraction;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("상호작용");
            OnInteraction?.Invoke();
            DoEnterInteraction();
        }
    }
    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DoStayInteraction();
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DoExitInteraction();
        }
    }

    public abstract void DoEnterInteraction();
    public abstract void DoStayInteraction();
    public abstract void DoExitInteraction();
}
