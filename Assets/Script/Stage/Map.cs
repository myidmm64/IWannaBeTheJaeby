using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Map : MonoBehaviour
{
    [field: SerializeField]
    private UnityEvent OnMapReset = null;
    [field: SerializeField]
    private UnityEvent OnMapStarted = null;

    public virtual void Init()
    {
        OnMapReset?.Invoke();

        OnMapStarted?.Invoke();
    }
}
