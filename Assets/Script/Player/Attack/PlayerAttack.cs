using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AgentMeleeAttack
{
    [SerializeField]
    private GameObject _bullet = null;

    [SerializeField]
    private bool _filp = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Attack();

            SpawnBullet();
        }
    }

    public void FilpCheck(bool val)
    {
        _filp = val;
    }

    private void SpawnBullet()
    {
        GameObject bullet = null;
        if (_filp)
        {
            bullet = Instantiate(_bullet, transform.position, Quaternion.Euler(0f, 180f, 0f));
        }
        else
        {
            bullet = Instantiate(_bullet, transform.position, Quaternion.Euler(0f, 0f, 0f));
        }
        Destroy(bullet, 2f);
    }
}
