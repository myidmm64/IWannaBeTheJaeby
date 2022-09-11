using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AgentMeleeAttack
{
    [SerializeField]
    private Transform _bulletSpawnPos = null;
    [SerializeField]
    private bool _isAutoShoot = true;
    [SerializeField]
    private float _reloadTime = 0.2f;
    [SerializeField]
    private int _bulletPerSec = 5;
    private int _bulletCnt = 0;
    private float _bulletTimer = 0f;

    private float _timer = 0f;

    [SerializeField]
    private bool _filp = false;

    KeySetting _keySetting = null;

    private void Start()
    {
        if (_keySetting == null)
        {
            _keySetting = KeyManager.Instance.keySetting;
        }
    }

    void Update()
    {

        _bulletTimer += Time.deltaTime;

        if (_bulletTimer >= 1f)
        {
            _bulletCnt = 0;
            _bulletTimer = 0f;
        }

        if (_isAutoShoot)
        {
            if (Input.GetKey(_keySetting.Keys[KeyAction.ATTACK]))
            {
                _timer += Time.deltaTime;

                if (_timer >= _reloadTime && _bulletCnt < _bulletPerSec)
                {
                    Attack();

                    SpawnBullet();

                    _bulletCnt++;
                    _timer = 0f;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(_keySetting.Keys[KeyAction.ATTACK]))
            {
                Attack();

                SpawnBullet();
            }
        }

    }

    public void FilpCheck(bool val)
    {
        _filp = val;
    }

    private void SpawnBullet()
    {

        BulletMove bullet = null;
        if (_filp)
        {
            bullet = PoolManager.Instance.Pop("Bullet") as BulletMove;
            bullet.gameObject.SetActive(true);
            bullet.transform.SetPositionAndRotation(_bulletSpawnPos.position, Quaternion.Euler(0f, 180f, 0f));
        }
        else
        {
            bullet = PoolManager.Instance.Pop("Bullet") as BulletMove;
            bullet.gameObject.SetActive(true);
            bullet.transform.SetPositionAndRotation(_bulletSpawnPos.position, Quaternion.Euler(0f, 0f, 0f));
        }
    }
}
