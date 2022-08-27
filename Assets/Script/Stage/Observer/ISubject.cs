using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
    public void Stream(SubjectState sub);
    public void Sub(ObserverableObject observer);
    public void DisSub(ObserverableObject observer);
}

[System.Serializable]
public enum SubjectState
{
    JUMP,
    MOVE,
    ATTACK,
    DASH,
    RESTART
}
