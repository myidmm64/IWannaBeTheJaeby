using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class KeySettingUI : MonoBehaviour
{
    [SerializeField]
    private StartSceneManager _startSceneManager = null;
    private TextMeshProUGUI _text;
    Event keyEvent;

    KeyDataClass _keyDataClass = new KeyDataClass();

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
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < (int)KeyAction.SIZE; i++)
        {
            _text.SetText($"< {((KeyAction)i).ToString()} >");
            yield return new WaitUntil(() => keyEvent.isKey);
            if (keyEvent.keyCode == KeyCode.None)
            {
                i--;
                continue;
            };
            _keyDataClass.KeyDatas.Add(new KeyData
            {
                key = (KeyAction)i,
                value = keyEvent.keyCode
            });
            yield return new WaitForSeconds(0.2f);
        }
        SaveKeyData();
        //< Å° º¯°æ >
        _startSceneManager.LockKey = false;
        _text.SetText("< Key Setting >");
    }

    private void SaveKeyData()
    {
        string path = Application.dataPath + "/Save/KeyData.json";
        string json = JsonUtility.ToJson(_keyDataClass);
        Debug.Log(json);
        File.WriteAllText(path, json);
    }

    private void OnGUI()
    {
        keyEvent = Event.current;
    }
}

[System.Serializable]
public class KeyDataClass
{
    public List<KeyData> KeyDatas = new List<KeyData>();
}
