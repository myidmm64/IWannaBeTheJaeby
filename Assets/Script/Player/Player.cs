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


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            OnSaveButton?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnRevertSaveButton?.Invoke();
        }
    }
}
