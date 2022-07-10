using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MousePos : MonoBehaviour
{
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int X, int Y);

    public GameObject somethingToTest;

    private void Start()
    {
        Vector3 tmpScreenPos = Camera.main.WorldToScreenPoint(somethingToTest.transform.position);
        SetCursorPos((int)tmpScreenPos.x, Screen.height - (int)tmpScreenPos.y);
    }
}