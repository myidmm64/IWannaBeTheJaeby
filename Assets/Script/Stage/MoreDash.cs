using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreDash : MoreJump
{
    protected override void DoSome(Collider2D col)
    {
        base.DoSome(col);
        Save.Instance.playerMovemant.MoreDash();
    }
}
