using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [field: SerializeField]
    private UnityEvent OnSaveButton = null;
    [field: SerializeField]
    private UnityEvent OnRevertSaveButton = null;

    KeySetting _keySetting = null;

    private void Start()
    {
        if (_keySetting == null)
        {
            _keySetting = KeyManager.Instance.keySetting;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(_keySetting.Keys[KeyAction.RESTART]))
        {
            OnSaveButton?.Invoke();
        }
        if (Input.GetKeyDown(_keySetting.Keys[KeyAction.SAVE_REVERT]))
        {
            OnRevertSaveButton?.Invoke();
        }
    }
}
