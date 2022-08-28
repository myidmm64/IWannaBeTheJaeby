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
    private Transform _target = null;
    [SerializeField]
    private float _duration = 5f;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void StartEnding()
    {
        if (Save.Instance.playerMovemant.gameObject.activeSelf == false) return;

        _text.enabled = true;
        Save.Instance.Saveable = false;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMove(_target.position, _duration));
        seq.AppendCallback(() =>
        {
            SceneManager.LoadScene(1);
        });

    }
}
