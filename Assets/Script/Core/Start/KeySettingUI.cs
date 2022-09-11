using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeySettingUI : MonoBehaviour
{
    [SerializeField]
    private StartSceneManager _startSceneManager = null;
    private TextMeshProUGUI _text;
    private KeyCode key;
    Event keyEvent;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void KeySet()
    {
        StartCoroutine(KeySettingCoroutine());
    }

    private IEnumerator KeySettingCoroutine()
    {
        _startSceneManager.LockKey = true;
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < (int)KeyAction.SIZE; i++)
        {
            _text.SetText($"< {((KeyAction)i).ToString()} >");
            yield return new WaitUntil(() => Input.anyKeyDown);
            KeySetting.keys[(KeyAction)i] = key;
            Debug.Log($"µñ¼Å {KeySetting.keys[(KeyAction)i]}");
            yield return new WaitForSeconds(0.3f);
        }
        //< Å° º¯°æ >
        _startSceneManager.LockKey = false;
        _text.SetText("< Å° º¯°æ >");
    }

    private void OnGUI()
    {
        keyEvent = Event.current;
        if(keyEvent.isKey)
        {
            key = keyEvent.keyCode;
            Debug.Log(key);
        }
    }
}
