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
    private GameObject _bossObject = null;
    private bool _bossStarted = false;



    public void BossStart()
    {
        if (_isFirst)
        {
            _originPos = new Vector3[2];
            _isFirst = false;
            _playerImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.currentResolution.width);
            _playerImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.currentResolution.height * 0.5f);
            _bossImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.currentResolution.width);
            _bossImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.currentResolution.height * 0.5f);
            _playerImage.anchoredPosition = new Vector2(Screen.currentResolution.width * -1f, 0f);
            _bossImage.anchoredPosition = new Vector2(Screen.currentResolution.width, 0f);

            _originPos[0] = _playerImage.anchoredPosition;
            _originPos[1] = _bossImage.anchoredPosition;
        }

        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();
        _seq.AppendInterval(0.2f);
        _seq.Append(_playerImage.DOAnchorPosX(0f, 1f));
        _seq.AppendCallback(() =>
        {
            CameraManager.instance.CameraShake(4f, 20f, 1f);
        });
        _seq.Append(_bossImage.DOAnchorPosX(0f, 1f));
        _seq.AppendCallback(() =>
        {
            CameraManager.instance.CameraShake(4f, 20f, 1f);
        });
        _seq.Append(_playerImage.DOAnchorPosX(_originPos[0].x, 1f));
        _seq.Join(_bossImage.DOAnchorPosX(_originPos[1].x, 1f));
        _seq.AppendCallback(() =>
        {
            BossSpawn();
        });
    }



    private void BossSpawn()
    {
        _bossObject.SetActive(true);
        _bossObject.GetComponent<MengueBoss>().BossStart();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (_bossStarted == false)
            {
                _bossStarted = true;
                if (_seq != null)
                    _seq.Kill();
                _playerImage.anchoredPosition = _originPos[0];
                _bossImage.anchoredPosition = _originPos[1];

                BossSpawn();
            }
        }
    }

    private void OnDisable()
    {
        if (_seq != null)
            _seq.Kill();

        _bossStarted = false;
        _playerImage.anchoredPosition = _originPos[0];
        _bossImage.anchoredPosition = _originPos[1];

        _bossObject.SetActive(false);
    }
}
