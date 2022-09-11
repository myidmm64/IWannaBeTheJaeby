using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyAction
{
    LEFT,
    RIGHT,
    JUMP,
    ATTACK,
    DASH,
    SAVE,
    SAVEREVERT,
    SIZE
}

public static class KeySetting
{
    public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>();
}

public class KeyManager : MonoBehaviour
{
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
        for(int i = 0; i < (int)KeyAction.SIZE; i++)
        {
            KeySetting.keys.Add((KeyAction)i, defaultKeys[i]);
        }
    }
}
