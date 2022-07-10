using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MousePos : MonoBehaviour
{
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int X, int Y);

    [SerializeField]
    private GameObject _target = null;
    [SerializeField]
    private float _speed = 2f;

    public GameObject somethingToTest;

    private void Start()
    {
        //Vector3 tmpScreenPos = Camera.main.WorldToScreenPoint(somethingToTest.transform.position);
        //SetCursorPos((int)tmpScreenPos.x, Screen.height - (int)tmpScreenPos.y);
    }

    private void Update()
    {
        Vector3 tmpScreenPos = Camera.main.WorldToScreenPoint(somethingToTest.transform.position);
        SetCursorPos((int)tmpScreenPos.x, Screen.height - (int)tmpScreenPos.y);
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3((int)tmpScreenPos.x, Screen.height - (int)tmpScreenPos.y, 0f));
    }


}