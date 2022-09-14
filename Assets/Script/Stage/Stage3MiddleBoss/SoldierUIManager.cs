using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class SoldierUIManager : MonoBehaviour
{
    [SerializeField]
    private Image _firstWordImage = null;
    [SerializeField]
    private Image _secondWordImage = null;
    [SerializeField]
    private Image _thirdWordImage = null;

    [SerializeField]
    private TextMeshProUGUI _firstWordText = null;
    [SerializeField]
    private TextMeshProUGUI _secondWordText = null;
    [SerializeField]
    private TextMeshProUGUI _thirdWordText = null;

    [SerializeField]
    private GameObject _firstLinkObject = null;
    [SerializeField]
    private GameObject _secondLinkObject = null;

    private Sequence _seq = null;

    public void SetText(string first, string second, string third, float callbackTime , Action Callback)
    {
        if (_seq != null)
        {
            _seq.Kill();
            _firstWordImage.transform.localScale = Vector3.one;
            _secondWordImage.transform.localScale = Vector3.one;
            _thirdWordImage.transform.localScale = Vector3.one;
        }
        _firstWordText.SetText(first);
        _secondWordText.SetText(second);
        _thirdWordText.SetText(third);

        _seq = DOTween.Sequence();
        _firstWordImage.transform.localScale = Vector3.one * 1.5f;
        _secondWordImage.transform.localScale = Vector3.one * 1.5f;
        _thirdWordImage.transform.localScale = Vector3.one * 1.5f;

        _firstWordImage.gameObject.SetActive(true);
        _seq.Append(_firstWordImage.transform.DOScale(1f, 0.5f));
        _seq.AppendCallback(() =>
        {
            CameraManager.instance.CameraShake(20f, 4f, 0.2f);
            _secondWordImage.gameObject.SetActive(true);
        });
        _seq.Append(_secondWordImage.transform.DOScale(1f, 0.5f));
        _seq.AppendCallback(() =>
        {
            CameraManager.instance.CameraShake(20f, 4f, 0.2f);
            _thirdWordImage.gameObject.SetActive(true);
            _firstLinkObject.SetActive(true);
        });
        _seq.Append(_thirdWordImage.transform.DOScale(1f, 0.5f));
        _seq.AppendCallback(() =>
        {
            CameraManager.instance.CameraShake(20f, 4f, 0.2f);
            _secondLinkObject.SetActive(true);
        });
        _seq.AppendInterval(callbackTime);
        _seq.AppendCallback(() =>
        {
            Callback?.Invoke();
        });
    }

    public void ResetUI()
    {
        if (_seq != null)
        {
            _seq.Kill();
            _firstWordImage.transform.localScale = Vector3.one;
            _secondWordImage.transform.localScale = Vector3.one;
            _thirdWordImage.transform.localScale = Vector3.one;
        }
        StopAllCoroutines();
        _firstWordImage.gameObject.SetActive(false);
        _secondWordImage.gameObject.SetActive(false);
        _thirdWordImage.gameObject.SetActive(false);
        _firstLinkObject.SetActive(false);
        _secondLinkObject.SetActive(false);
    }

    private void OnDisable()
    {
        ResetUI();
    }
}
