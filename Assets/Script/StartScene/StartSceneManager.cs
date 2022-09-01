using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class StartSceneManager : MonoBehaviour
{
    private TextMeshProUGUI _current = null;
    [SerializeField]
    private List<TextMeshProUGUI> _UIs = null;
    private int _selectNum = 0;
    private bool _lockKey = false;
    public bool LockKey
    {
        get => _lockKey;
        set => _lockKey = value;
    }

    private int _lastSelec = -1;

    Sequence _seq = null;

    private void Start()
    {
        _UIs[0].transform.localPosition = Vector3.zero;
        ImpactText(_UIs[0]);
    }

    private void Update()
    {
        if (_lockKey) return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            DownUI();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            UpUI();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LeftUI();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RightUI();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            _current.GetComponent<StartSceneText>().Excute();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            _current.GetComponent<StartSceneText>().Excute();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _current.GetComponent<StartSceneText>().Return();
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            _current.GetComponent<StartSceneText>().Return();
        }
    }

    private void UpUI()
    {
        _lastSelec = _selectNum;
        _selectNum++;
        _selectNum = Mathf.Clamp(_selectNum, 0, _UIs.Count-1);
        ImpactText(_UIs[_selectNum]);
    }

    private void DownUI()
    {
        _lastSelec = _selectNum;
        _selectNum--;
        _selectNum = Mathf.Clamp(_selectNum, 0, _UIs.Count-1);
        ImpactText(_UIs[_selectNum]);
    }

    private void LeftUI()
    {
        StartSceneText text = _current.GetComponent<StartSceneText>();
        if (text != null)
        {
            text.LeftAction();
        }
    }
    private void RightUI()
    {
        StartSceneText text = _current.GetComponent<StartSceneText>();
        if (text != null)
        {
            text.RightAction();
        }
    }

    private void ImpactText(TextMeshProUGUI text)
    {
        if (_lastSelec == _selectNum) return;

        _lockKey = true;
        _seq = DOTween.Sequence();
        if (_current != null)
        {
            //_current.transform.rotation = Quaternion.identity;
            _seq.Append(_current.transform.DOLocalMove(Vector3.zero, 0.1f));
            _current.transform.rotation = Quaternion.identity;
            _current.fontSize = 40f;

            StartSceneText currentText = _current.GetComponent<StartSceneText>();
            currentText?.Exit();
        }

        //text.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 10f));
        _seq.Join(text.transform.DOLocalMove(Vector3.right * 35f, 0.1f));
        text.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 7f));
        text.fontSize = 60f;
        _current = text;

        StartSceneText nextText = _current.GetComponent<StartSceneText>();
        nextText?.Enter();

        _seq.AppendCallback(() =>
        {
            _lockKey = false;
        });
    }

    public void ResetList(List<TextMeshProUGUI> textList)
    {
        if (_seq != null)
            _seq.Kill();


        for (int i = 0; i<_UIs.Count; i++)
        {
            _UIs[i].transform.position = Vector3.zero;
            _UIs[i].transform.rotation = Quaternion.identity;
            _UIs[i].fontSize = 40f;
        }

        StartSceneText currentText = _current.GetComponent<StartSceneText>();
        currentText?.Exit();
        _current = null;
        _UIs.Clear();
        _UIs.AddRange(textList);
        _UIs[0].transform.localPosition = Vector3.zero;
        ImpactText(_UIs[0]);

        _selectNum = 0;
        _lastSelec = -1;
        _lockKey = false;
    }

    private void OnDestroy()
    {
        if (_seq != null)
            _seq.Kill();
        StopAllCoroutines();
    }
}
