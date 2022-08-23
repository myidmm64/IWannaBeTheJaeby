using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class MengueBoss : Boss
{
    private SpriteRenderer _spriteRenderer = null;
    private Collider2D _col = null;
    private BossDamaged _damaged = null;

    [SerializeField]
    private GameObject _sword = null;
    private Animator _swordAnimator = null;
    [SerializeField]
    private Transform[] _swordTrms = null;
    [SerializeField]
    private Transform[] _bossTrms = null;
    [SerializeField]
    private Transform[] _jumpTrms = null;
    [SerializeField]
    private GameObject[] _enemys = null;

    private Collider2D _swordCol = null;
    private SpriteRenderer _swordSprite = null;

    private Sequence _seq = null;

    private bool _isFirst = true;
    private Vector3 _originPos = Vector3.zero;

    private void Awake()
    {
        _col = transform.Find("AgentSprite").GetComponent<Collider2D>();
        _spriteRenderer = _col.GetComponent<SpriteRenderer>();
        _damaged = _col.GetComponent<BossDamaged>();
        _swordAnimator = _sword.GetComponent<Animator>();
    }

    private void Start()
    {
        if (_isFirst)
        {
            _isFirst = false;
            _originPos = transform.position;
        }
        _damaged.ResetHP();
    }

    public void BossStart()
    {
        BossRoutine();
    }

    private void BossRoutine()
    {
        StartCoroutine(Pattern0());
    }

    private IEnumerator Pattern0()
    {
        _swordSprite = _sword.GetComponent<SpriteRenderer>();
        _swordCol = _sword.GetComponent<Collider2D>();
        _swordSprite.enabled = false;
        _swordCol.enabled = false;

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < _swordTrms.Length; i++)
        {
            _swordSprite.enabled = false;
            _swordCol.enabled = false;
            transform.DOMove(_bossTrms[i].position, 0.5f);
            yield return new WaitForSeconds(0.5f);
            if(i == _swordTrms.Length - 1)
                yield return new WaitForSeconds(1.2f);

            _swordSprite.enabled = true;
            _swordCol.enabled = true;

            _sword.transform.position = _swordTrms[i].position;

            _swordAnimator.SetTrigger("Slash" + i);
            CameraManager.instance.CameraShake(2f, 30f, 0.6f);
            yield return new WaitForSeconds(0.7f);
        }

        _swordSprite.enabled = false;
        _swordCol.enabled = false;
        transform.DOMove(_bossTrms[_swordTrms.Length].position, 1f).OnComplete(() => { Pattern1(); });
    }

    private void Pattern1()
    {
        if (_seq != null)
            _seq.Kill();
        transform.DOKill();

        transform.position = _jumpTrms[0].position - Vector3.down * 5f;
        _seq = DOTween.Sequence();
        _seq.Append(transform.DOMove(_jumpTrms[0].position, 0.5f));
        _seq.Append(transform.DOJump(_jumpTrms[1].position, 13f, 1, 0.7f));
        _seq.AppendCallback(() =>
        {
            for(int i = 0; i<_enemys.Length; i++)
            {
                _enemys[i].SetActive(true);
            }
        });
        _seq.AppendInterval(0.5f);
        _seq.AppendCallback(() =>
        {
            _damaged.IsGodMode = true;
        });
        _seq.Append(transform.DOMove(_bossTrms[_swordTrms.Length].position, 0.5f));
        _seq.AppendCallback(() =>
        {
            _damaged.IsGodMode = false;

            if (_seq != null)
                _seq.Kill();
            Pattern2();
        });
    }

    private void Pattern2()
    {
        if (_seq != null)
            _seq.Kill();

        transform.position = new Vector3(0, 8f, 0f);
        _seq = DOTween.Sequence();
        _seq.Append(transform.DOMove(_originPos, 1f));
    }

    public void DieReset()
    {
        if (_col != null)
        {
            _col.enabled = false;
        }
        if (_seq != null)
            _seq.Kill();
    }

    public override void ResetBoss()
    {
        if (_col != null)
        {
            _col.enabled = true;
            _spriteRenderer.enabled = true;
            _damaged.ResetHP();
        }

        StopAllCoroutines();
        transform.DOKill();
        if (_seq != null)
            _seq.Kill();
        if (_originPos != Vector3.zero)
        {
            transform.position = _originPos;
            _sword.transform.position = _swordTrms[0].transform.position;
            _sword.transform.rotation = Quaternion.identity;
        }

        for (int i = 0; i < _enemys.Length; i++)
        {
            _enemys[i].SetActive(false);
        }
    }
}
