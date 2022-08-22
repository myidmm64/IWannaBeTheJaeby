using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Stage3BossManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform _playerImage = null;
    [SerializeField]
    private RectTransform _bossImage = null;
    private Sequence _seq = null;
    private bool _isFirst = true;
    private Vector3[] _originPos = null;

    [SerializeField]
    private GameObject[] _startBossObjects = null;


    private void OnEnable()
    {
        if(_isFirst)
        {
            _isFirst = false;
            _originPos[0] = _playerImage.position;
            _originPos[1] = _bossImage.position;
        }
        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();
        _seq.Append(_playerImage.DOMoveX(0f, 0.5f));
        _seq.AppendCallback(() =>
        {
            CameraManager.instance.CameraShake(4f, 20f, 0.5f);
        });
        _seq.Append(_bossImage.DOMoveX(0f, 0.5f));
        _seq.AppendCallback(() =>
        {
            CameraManager.instance.CameraShake(4f, 20f, 0.5f);
            for(int i =  0; i<_startBossObjects.Length; i++)
            {
                _startBossObjects[i].SetActive(true);
            }
        });
    }
    private void OnDisable()
    {
        if (_seq != null)
            _seq.Kill();

        _playerImage.position = _originPos[0];
        _bossImage.position = _originPos[1];

        for (int i = 0; i < _startBossObjects.Length; i++)
        {
            _startBossObjects[i].SetActive(false);
        }
    }
}
