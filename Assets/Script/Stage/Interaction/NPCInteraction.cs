using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class NPCInteraction : Interaction
{
    [SerializeField]
    private GameObject _keyManual = null;
    [SerializeField]
    private TextMeshPro _text = null;
    [SerializeField]
    private string[] _texts = null;
    [SerializeField]
    private float _voiceSpeed = 1.5f;
    [field: SerializeField]
    private UnityEvent OnEndTextCallback = null;

    private bool _startingVoice = false;
    private bool _voiceable = false;

    public override void DoEnterInteraction()
    {
    }

    public override void DoStayInteraction()
    {
    }

    private void OnEnable()
    {
        ResetVoice();
    }

    private void Update()
    {
        if(_voiceable)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartVoice();
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (_startingVoice) return;

        if(collision.CompareTag("Player"))
        {
            _voiceable = true;
            _keyManual.SetActive(true);

        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (_startingVoice) return;

        if (collision.CompareTag("Player"))
        {
            _voiceable = false;
            _keyManual.SetActive(false);
        }
    }

    private void StartVoice()
    {
        StartCoroutine(VoiceCoroutine());
    }

    private IEnumerator VoiceCoroutine()
    {
        _keyManual.SetActive(false);
        _startingVoice = true;
        _voiceable = false;

        for (int i = 0; i<_texts.Length; i++)
        {
            _text.SetText(_texts[i]);
            yield return new WaitForSeconds(_voiceSpeed);
        }
        OnEndTextCallback?.Invoke();
        ResetVoice();
    }

    public void ResetVoice()
    {
        StopAllCoroutines();
        _text.SetText("");
        _startingVoice = false;
        _voiceable = false;
    }

    public override void DoExitInteraction()
    {
    }
}
