using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFire : MonoBehaviour
{
    [SerializeField]
    private GameObject _smallFire = null;

    private Rigidbody2D _rigid = null;

    private bool _isFilped = false;
    [SerializeField]
    private bool _isMoveFire = false;

    Vector3 cur = Vector3.zero;

    public void SpawnSmallDalgona()
    {
        GameObject dal = Instantiate(_smallFire, transform.parent);
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, 90f + Random.Range(10f, 20f)));
        dal.transform.SetPositionAndRotation(transform.position, rot);

        GameObject dal2 = Instantiate(_smallFire, transform.parent);
        Quaternion rot2 = Quaternion.Euler(new Vector3(0f, 0f, 90f + Random.Range(-10f, -20f)));
        dal2.transform.SetPositionAndRotation(transform.position, rot2);

        Destroy(dal, 2f);
        Destroy(dal2, 2f);
    }

    private void Start()
    {
        cur = transform.eulerAngles;
    }

    public void Init(float gravity)
    {
        _rigid = GetComponent<Rigidbody2D>();
        _rigid.gravityScale = gravity;
    }

    public void Flip()
    {
        CameraManager.instance.CameraShake(5f, 40f, 0.2f);
        transform.rotation = Quaternion.Euler(cur.x, cur.y, cur.z * -1f);
        _isFilped = true;
    }

    private void Update()
    {
        if (_isMoveFire == false) return;
        if (_rigid == null)
            _rigid = GetComponent<Rigidbody2D>();
        if (_isFilped == false) return;
        if (_rigid.velocity.y < 0f)
        {
            transform.rotation = Quaternion.Euler(cur);
            _isFilped = false;
        }
    }
}
