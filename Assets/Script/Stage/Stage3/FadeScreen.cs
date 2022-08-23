using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [SerializeField]
    private float _targetFade = 0f;
    [SerializeField]
    private float _duration = 0f;
    private Image _image = null;


    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void Fade()
    {
        _image.DOFade(_targetFade, _duration);
    }
    public void ResetFade()
    {
        _image.DOKill();
        _image.DOFade(0f, 0f);
    }
}
