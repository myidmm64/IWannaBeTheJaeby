using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : AgentDamage
{
    protected override void Die()
    {
        Debug.Log("�׾����");

        transform.root.gameObject.SetActive(false);
    }
}
