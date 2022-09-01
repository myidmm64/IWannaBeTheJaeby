using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class StartSceneText : MonoBehaviour
{
    [field: SerializeField]
    private UnityEvent OnLeftAction = null;
    [field: SerializeField]
    private UnityEvent OnRightAction = null;
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Excute();
    public abstract void Return();

    public virtual void LeftAction()
    {
        OnLeftAction?.Invoke();
    }

    public virtual void RightAction()
    {
        OnRightAction?.Invoke();
    }
}
