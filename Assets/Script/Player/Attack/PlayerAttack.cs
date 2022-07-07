using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AgentMeleeAttack
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Attack();
        }
    }
}
