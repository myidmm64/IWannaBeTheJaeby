using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Difficulty difficulty = Difficulty.Easy;

    public enum Difficulty
    {
        None,
        Easy,
        Normal,
        Hard,
        Extreme
    }

    [field: SerializeField]
    private UnityEvent OnSaveButton = null;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            OnSaveButton?.Invoke();
        }
    }
}
