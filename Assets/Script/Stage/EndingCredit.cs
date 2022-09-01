using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class EndingCredit : MonoBehaviour
{
    private TextMeshProUGUI _text = null;
    [SerializeField]
    private RectTransform _target = null;
    private RectTransform _trm = null;
    [SerializeField]
    private float _duration = 5f;

    private void Awake()
    {
        _trm = GetComponent<RectTransform>();
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void StartEnding()
    {
        _text.enabled = true;
        Save.Instance.Saveable = false;
        Sequence seq = DOTween.Sequence();
        seq.Append(_trm.DOMove(_target.position, _duration));
        seq.AppendCallback(() =>
        {
            seq.Kill();
            DOTween.KillAll();
            SceneManager.LoadScene(0);
        });

    }
}
