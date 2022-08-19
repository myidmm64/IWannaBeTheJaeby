using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DifferentTextChange : StartSceneText
{
    [SerializeField]
    private List<TextMeshProUGUI> _nextTexts = null;
    [SerializeField]
    private List<TextMeshProUGUI> _pastTexts = null;

    [SerializeField]
    private bool _onlyEventNext = false;
    [SerializeField]
    private bool _onlyEventPast = false;

    [field: SerializeField]
    private UnityEvent OnNextText = null;
    [field: SerializeField]
    private UnityEvent OnPastText = null;

    private StartSceneManager _startSceneManager = null;

    private void Awake()
    {
        _startSceneManager = GameObject.FindObjectOfType<StartSceneManager>();
    }

    public override void Excute()
    {
        if(_onlyEventNext)
        {
            OnNextText?.Invoke();
            return;
        }
        if (_nextTexts.Count == 0) return;
        for (int i = 0; i < _nextTexts.Count; i++)
        {
            _nextTexts[i].enabled = true;
            _nextTexts[i].transform.localPosition = Vector3.zero;
        }
        OnNextText?.Invoke();
        _startSceneManager.ResetList(_nextTexts);
    }

    public override void Return()
    {
        if (_onlyEventPast)
        {
            OnPastText?.Invoke();
            return;
        }
        if (_pastTexts.Count == 0) return;
        for (int i = 0; i < _pastTexts.Count; i++)
        {
            _pastTexts[i].enabled = true;
            _pastTexts[i].transform.localPosition = Vector3.zero;
        }
        OnPastText?.Invoke();
        _startSceneManager.ResetList(_pastTexts);
    }
}
