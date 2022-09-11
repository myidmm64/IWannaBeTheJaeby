using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Achievements : MonoBehaviour
{
    private Vector2 _originAnchoredPos = Vector2.zero;
    private RectTransform _trm = null;
    [SerializeField]
    private RectTransform _endPosTrm = null;
    private Vector2 _endPos = Vector2.zero;
    private Sequence _seq = null;

    private bool _isFirst = true;

    private void Start()
    {
        _trm = GetComponent<RectTransform>();
        _originAnchoredPos = _trm.anchoredPosition;
        _endPos = _endPosTrm.anchoredPosition;
        Debug.Log($"{_originAnchoredPos}, {_endPos}");
    }

    [ContextMenu("ÆË¾÷")]
    public void Popup()
    {
        if (_seq != null)
        {
            _seq.Kill();
            _trm.anchoredPosition = _originAnchoredPos;
        }

        if(_isFirst)
        {
            _isFirst = false;
            PlayerPrefs.SetInt("SAVE_ACHIEVEMENT", PlayerPrefs.GetInt("SAVE_ACHIEVEMENT", 0) + 1);
        }

        _seq = DOTween.Sequence();
        _seq.Append(_trm.DOAnchorPos(_endPos, 1f));
        _seq.AppendInterval(1.5f);
        _seq.Append(_trm.DOAnchorPos(_originAnchoredPos, 0.5f));
    }
}
