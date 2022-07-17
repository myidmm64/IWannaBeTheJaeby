using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interaction : MonoBehaviour
{
    [field: SerializeField]
    private UnityEvent OnInteraction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("상호작용");
            OnInteraction?.Invoke();
            DoInteraction();
        }
    }

    public abstract void DoInteraction();
}
