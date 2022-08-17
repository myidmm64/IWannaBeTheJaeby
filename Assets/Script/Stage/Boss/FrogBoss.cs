using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FrogBoss : Boss
{
    [SerializeField]
    private Transform _startPos = null;
    private Sequence _seq = null;
    private Rigidbody2D _rigid = null;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void StartBoss()
    {
        StartCoroutine(BossCoroutine());
    }

    private IEnumerator BossCoroutine()
    {
        _rigid.gravityScale = 1f;
        yield return new WaitForSeconds(2.5f);
        _rigid.gravityScale = 0f;
        _seq = DOTween.Sequence();
        _seq.Append(transform.DOJump(transform.position + Vector3.right * 4f, 4f, 1, 1f));
    }

    public override void ResetBoss()
    {
        if (_seq != null)
            _seq.Kill();
        StopAllCoroutines();
        transform.position = _startPos.position;
    }
}
