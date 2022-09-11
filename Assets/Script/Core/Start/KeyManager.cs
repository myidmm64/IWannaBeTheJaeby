using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public enum KeyAction
{
    LEFT,
    RIGHT,
    JUMP,
    ATTACK,
    DASH,
    RESTART,
    SAVEREVERT,
    SIZE
}

[System.Serializable]
public class KeySetting
{
    private Dictionary<KeyAction, KeyCode> _keys = new Dictionary<KeyAction, KeyCode>();
    public Dictionary<KeyAction, KeyCode> Keys
    {
        get => _keys;
        set => _keys = value;
    }
}

[System.Serializable]
public struct KeyData
{
    public KeyAction key;
    public KeyCode value;
}

public class KeyManager : MonoBehaviour
{
    private KeySetting _keySetting = new KeySetting();

    KeyCode[] defaultKeys = new KeyCode[]
    {
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.C,
        KeyCode.X,
        KeyCode.Z,
        KeyCode.R,
        KeyCode.P,
    };

    private void Awake()
    {
        LoadKeyData();
    }

    private void LoadKeyData()
    {
        string path = Application.dataPath + "/Save/KeyData.json";
        KeyDataClass keyData = new KeyDataClass();
        keyData = JsonUtility.FromJson<KeyDataClass>(File.ReadAllText(path));
        if(keyData.KeyDatas.Count != (int)KeyAction.SIZE)
        {
            for(int i = 0; i<(int)KeyAction.SIZE; i++)
            {
                _keySetting.Keys.Add((KeyAction)i, defaultKeys[i]);
            }
        }
        else
        {
            for (int i = 0; i < keyData.KeyDatas.Count; i++)
            {
                _keySetting.Keys.Add(keyData.KeyDatas[i].key, keyData.KeyDatas[i].value);
            }
        }

        for (int i = 0; i < (int)KeyAction.SIZE; i++)
        {
            Debug.Log(_keySetting.Keys[(KeyAction)i].ToString());
        }
    }
}
