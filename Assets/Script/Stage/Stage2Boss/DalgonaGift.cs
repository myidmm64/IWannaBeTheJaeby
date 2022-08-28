using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DalgonaGift : MonoBehaviour
{
    [SerializeField]
    private GameObject _bigDalgona = null;
    private Sequence _seq = null;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
        Vector3 origin = transform.position;

        _seq = DOTween.Sequence();
        _seq.Append(transform.DOMoveX(origin.x + 3f, 0.35f).SetEase(Ease.Linear));
        _seq.Append(transform.DOMoveX(origin.x, 0.35f).SetEase(Ease.Linear));
        _seq.Append(transform.DOMoveX(origin.x - 3f, 0.35f).SetEase(Ease.Linear)).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        if(_seq != null)
        {
            StopAllCoroutines();
            _seq.Kill();
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 4; i++)
        {
            GameObject big = Instantiate(_bigDalgona, transform.position, Quaternion.identity);
            big.transform.SetParent(transform.parent);
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 4; i++)
        {
            GameObject big = Instantiate(_bigDalgona, transform.position, Quaternion.identity);
            big.transform.SetParent(transform.parent);
            yield return new WaitForSeconds(0.5f);
        }
        if(_seq != null)
            _seq.Kill();
        Destroy(gameObject);
    }
}
