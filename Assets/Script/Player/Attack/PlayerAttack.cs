using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AgentMeleeAttack
{
    [SerializeField]
    private GameObject _bullet = null;

    private bool _filp = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Attack();
        }
    }

    public void SpawnBullet(bool val)
    {
        _filp = val;
        if(val)
        {
            Instantiate(_bullet, transform.position, Quaternion.Euler(0f, 180f, 0f));
        }
        else
        {
            Instantiate(_bullet, transform.position, Quaternion.Euler(0f, 0f, 0f));
        }
    }
}
