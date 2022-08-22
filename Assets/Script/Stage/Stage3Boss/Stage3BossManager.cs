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
    private bool _bossStarted = false;



    public void BossStart()
    {
        if (_isFirst)
        {
            _originPos = new Vector3[2];
            _isFirst = false;
            _playerImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
            _playerImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height * 0.5f);
            _bossImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
            _bossImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height * 0.5f);

            _playerImage.anchoredPosition = new Vector2(-Screen.width, 0f);
            _bossImage.anchoredPosition = new Vector2(Screen.width, 0f);

            _originPos[0] = _playerImage.position;
            _originPos[1] = _bossImage.position;
        }

        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();
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
        for (int i = 0; i < _startBossObjects.Length; i++)
        {
            _startBossObjects[i].SetActive(true);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            if(_bossStarted == false)
            {
                _bossStarted = true;
                if (_seq != null)
                    _seq.Kill();
                _playerImage.position = _originPos[0];
                _bossImage.position = _originPos[1];

                BossSpawn();
            }
        }
    }

    private void OnDisable()
    {
        if (_seq != null)
            _seq.Kill();

        _bossStarted = false;
        _playerImage.position = _originPos[0];
        _bossImage.position = _originPos[1];

        for (int i = 0; i < _startBossObjects.Length; i++)
        {
            _startBossObjects[i].SetActive(false);
        }
    }
}
