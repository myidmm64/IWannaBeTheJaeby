using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PopupText : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _text;

    [SerializeField]
    private Transform _targetTrm = null;
    [SerializeField]
    private Vector2 _movePos = Vector2.zero;
    [SerializeField]
    private float _duration = 1f;

    private Sequence seq;

    public void Popup()
    {
        if (seq != null)
            seq.Kill();

        _text.transform.position = _targetTrm.position;
        _text.enabled = true;
        seq = DOTween.Sequence();
        seq.Append(_text.transform.DOMove(_text.transform.position + (Vector3)_movePos, _duration));
        seq.AppendCallback(() => { _text.enabled = false; });
    }

}
