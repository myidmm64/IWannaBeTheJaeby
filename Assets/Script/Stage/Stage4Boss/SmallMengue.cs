using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class SmallMengue : MonoBehaviour
{
    private Vector3 _originPos = Vector3.zero;

    private Sequence _seq = null;
    [SerializeField]
    private List<GameObject> _coins = new List<GameObject>();
    [field: SerializeField]
    private UnityEvent OnPatternEnd = null;

    private void OnEnable()
    {
        if(_originPos == Vector3.zero)
        {
            _originPos = transform.position;
        }
        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();
        _seq.Append(transform.DOMoveY(1.34f, 0.5f));
        _seq.AppendCallback(() =>
        {
            MengStart();
        });
    }

    private void MengStart()
    {
        StartCoroutine(SpawnCoinCoroutine());
    }

    private IEnumerator SpawnCoinCoroutine()
    {
        for(int i = 0; i<_coins.Count; i++)
        {
            _coins[i].SetActive(true);
            yield return new WaitForSeconds(1f);
        }

        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();
        _seq.Append(transform.DOMoveY(_originPos.y, 0.5f));
        _seq.AppendInterval(4f);
        _seq.AppendCallback(() =>
        {
            OnPatternEnd?.Invoke();
        });
    }

    private void OnDisable()
    {
        if (_seq != null)
            _seq.Kill();
        StopAllCoroutines();

        for (int i = 0; i < _coins.Count; i++)
        {
            _coins[i].SetActive(false);
        }
        if (_originPos != Vector3.zero)
            transform.position = _originPos;
    }
}
