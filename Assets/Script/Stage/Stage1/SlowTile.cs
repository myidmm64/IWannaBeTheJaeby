using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTile : MonoBehaviour
{
    [SerializeField, Range(0.1f, 1f)]
    private float _slowXSpeed = 0.5f;
    [SerializeField, Range(0.1f, 1f)]
    private float _slowYSpeed = 0.8f;
    private Rigidbody2D _rigid = null;
    private Vector2 _slowAmount = Vector2.zero;

    private void FixedUpdate()
    {
        if(_rigid != null)
        {
            _slowAmount.x = _slowXSpeed * _rigid.velocity.x;
            _slowAmount.y = _slowYSpeed * _rigid.velocity.y;

            _rigid.velocity = _slowAmount;
        }
    }

    private void OnEnable()
    {
        _rigid = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            if (_rigid == null)
            {
                _rigid = Save.Instance.playerMovemant.GetComponent<Rigidbody2D>();
            }
                
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (_rigid != null)
            {
                _rigid = null;
            }
        }
    }
}
