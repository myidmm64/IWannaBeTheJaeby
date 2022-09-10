using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ForceJump : MoreJump
{
    [SerializeField]
    private float _power = 10f;

    protected override void DoSome(Collider2D col)
    {
        base.DoSome(col);
        col.transform.parent.GetComponent<AgentJump>().ForceJump(_power, Vector3.up, 2);
    }
}
