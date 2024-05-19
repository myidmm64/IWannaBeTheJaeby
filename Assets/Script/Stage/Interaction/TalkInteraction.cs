using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using DG.Tweening;

public class TalkInteraction : Interaction
{
    [SerializeField]
    private Sprite _playerSprite = null;
    [SerializeField]
    private Sprite _wineSprite = null;
    [SerializeField]
    private Sprite _girlSprite = null;
    [SerializeField]
    private Image _leftImage = null;
    [SerializeField]
    private Image _rightImage = null;
    [SerializeField]
    private TextMeshProUGUI _nameText = null;
    [SerializeField]
    private TextMeshProUGUI _contentText = null;
    [SerializeField]
    private GameObject _portal = null;
    [SerializeField]
    private GameObject _backgroundObject = null;

    [SerializeField]
    private List<TalkDatas> talkDatasList = new List<TalkDatas>();

    [SerializeField]
    private bool _isFirst = true;
    private bool _interactionStart = false;
    private bool _talkGenerating = false;
    private int _index = 0;

    private StringBuilder _sb = new StringBuilder();

    public override void DoEnterInteraction()
    {
        if (_isFirst)
        {
            _interactionStart = true;
            Save.Instance.Saveable = false;
            Save.Instance.playerMovemant.gameObject.SetActive(false);
            _isFirst = false;

            Sequence seq = DOTween.Sequence();
            seq.Append(_backgroundObject.transform.DOMoveY(0f, 1f));
            seq.AppendCallback(() =>
            {
                _contentText.gameObject.SetActive(true);
                _nameText.gameObject.SetActive(true);
                _nameText.transform.parent.gameObject.SetActive(true);
                TalkStart();
            });
        }
    }

    private void Start()
    {
        if (_isFirst == false)
        {
            _portal.SetActive(true);
        }
        else
        {
            _portal.SetActive(false);
        }

        _contentText.gameObject.SetActive(false);
        _nameText.gameObject.SetActive(false);
        _nameText.transform.parent.gameObject.SetActive(false);
    }

    private void TalkStart()
    {
        switch (talkDatasList[_index].spriteType)
        {
            case SpriteType.NONE:
                _nameText.text = "";
                _rightImage.gameObject.SetActive(false);
                _leftImage.gameObject.SetActive(false);
                break;
            case SpriteType.PLAYER:
                _leftImage.sprite = _playerSprite;
                _nameText.text = "Me";
                _leftImage.gameObject.SetActive(true);
                _rightImage.gameObject.SetActive(false);
                break;
            case SpriteType.WINE:
                _leftImage.sprite = _wineSprite;
                _nameText.text = "Wine";
                _leftImage.gameObject.SetActive(true);
                _rightImage.gameObject.SetActive(false);
                break;
            case SpriteType.GIRL:
                _rightImage.sprite = _girlSprite;
                _nameText.text = "A beauty with a glass of wine";
                _rightImage.gameObject.SetActive(true);
                _leftImage.gameObject.SetActive(false);
                break;
            default:
                break;
        }
        StartCoroutine(TalkGenerateCoroutine());
    }

    private IEnumerator TalkGenerateCoroutine()
    {
        _talkGenerating = true;
        string content = talkDatasList[_index].content;

        char[] charArray = content.ToCharArray();

        for (int i = 0; i < charArray.Length; i++)
        {
            _sb.Append(charArray[i]);
            _contentText.text = _sb.ToString();
            yield return new WaitForSeconds(0.05f);
        }
        _talkGenerating = false;

        _sb.Clear();
    }

    private void Update()
    {
        if (_interactionStart)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(_talkGenerating == false && _nameText.gameObject.activeSelf == true)
                {
                    _index++;
                    _index = Mathf.Clamp(_index, 0, talkDatasList.Count);
                    if(_index >= talkDatasList.Count)
                    {
                        TalkEnd();
                        return;
                    }
                    TalkStart();
                }
            }
        }
    }

    private void TalkEnd()
    {
        _contentText.gameObject.SetActive(false);
        _nameText.gameObject.SetActive(false);
        _nameText.transform.parent.gameObject.SetActive(false);
        _backgroundObject.transform.position = new Vector3(0f, -13f, 0f);
        Save.Instance.Saveable = true;
        Save.Instance.playerMovemant.gameObject.SetActive(true);
        _portal.gameObject.SetActive(true);
    }

    public void PortalOn()
    {
        if (_isFirst == false)
        {
            _portal.SetActive(true);
            _interactionStart = false;
        }
        else
        {
            _portal.SetActive(false);
        }
    }

    public override void DoExitInteraction()
    {
    }

    public override void DoStayInteraction()
    {
    }

    [System.Serializable]
    public struct TalkDatas
    {
        public SpriteType spriteType;
        public string content;
    }
    [System.Serializable]
    public enum SpriteType
    {
        NONE,
        PLAYER,
        WINE,
        GIRL,
    }
}
