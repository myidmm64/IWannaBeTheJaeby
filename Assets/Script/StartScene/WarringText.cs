using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class WarringText : MonoBehaviour
{
    [SerializeField]
    private RectTransform _targetPos = null;
    private Sequence _seq = null;
    private Vector3 _originPos = Vector3.zero;
    private TextMeshProUGUI _text = null;
    private RectTransform _trm = null;


    private void Awake()
    {
        _trm = GetComponent<RectTransform>();
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _originPos = _trm.position;
    }

    public void Warring(string text)
    {
        if (_seq != null)
            _seq.Kill();

        _trm.position = _originPos;
        _text.SetText(text);

        _seq = DOTween.Sequence();
        _seq.Append(transform.DOMoveX(_targetPos.position.x, 0.3f));
        _seq.AppendInterval(0.5f);
        _seq.Append(transform.DOMoveX(_originPos.x, 0.3f));
        _seq.AppendCallback(() =>
        {
            _text.SetText("");
        });
    }

    private void OnDestroy()
    {
        if (_seq != null)
            _seq.Kill();

        transform.DOKill();
    }

}
