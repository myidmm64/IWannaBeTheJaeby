using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFIre : MonoBehaviour
{
    private Rigidbody2D _rigid = null;
    private bool _isFirst = true;
    Vector3 dir = Vector3.zero;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        dir = Quaternion.AngleAxis(transform.rotation.eulerAngles.z - 90f, Vector3.forward) * Vector3.up;
        _rigid.AddForce(dir * Random.Range(6f, 8f), ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (_isFirst == false) return;
        if(_rigid.velocity.y < 0f)
        {
            _isFirst = false;
            transform.rotation = Quaternion.Euler(new Vector3(dir.x, dir.y, dir.z * -1f));
        }
    }
}
