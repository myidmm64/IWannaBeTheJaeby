using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour, ISubject
{
    [SerializeField]
    private List<ObserverableObject> observers = new List<ObserverableObject>();

    public void JumpStream()
    {
        Stream(SubjectState.JUMP);
    }
    public void AttackStream()
    {
        Stream(SubjectState.ATTACK);
    }
    public void DashStream()
    {
        Stream(SubjectState.DASH);
    }
    public void MoveStream()
    {
        Stream(SubjectState.MOVE);
    }
    public void RestartStream()
    {
        Stream(SubjectState.RESTART);
    }

    public void Stream(SubjectState sub)
    {
        for(int i = 0; i<observers.Count; i++)
        {
            if (observers[i].state == sub)
            {
                observers[i].observerObj.GetComponent<IObserver>().Observed();
            }
        }
    }

    public void Sub(ObserverableObject observer)
    {
        observers.Add(observer);
    }

    public void DisSub(ObserverableObject observer)
    {
        observers.Remove(observer);
    }
}

[Serializable]
public struct ObserverableObject
{
    public GameObject observerObj;
    public SubjectState state;
}
