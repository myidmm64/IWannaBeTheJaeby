using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoreJump : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer = null;
    private Collider2D _col = null;

    private Vector3 originPos = Vector3.zero;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        originPos = transform.position;

        transform.DOMoveY(originPos.y + 0.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(MoreJumpCoroutine());
        }
    }

    private void OnDisable()
    {
        if (_spriteRenderer == null || _col == null) return;

        _spriteRenderer.enabled = true;
        _col.enabled = true;
        transform.position = originPos;
    }

    private IEnumerator MoreJumpCoroutine()
    {
        _spriteRenderer.enabled = false;
        _col.enabled = false;
        yield return new WaitForSeconds(1.5f);
        _spriteRenderer.enabled = true;
        _col.enabled = true;
    }
}
