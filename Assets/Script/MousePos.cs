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

    Vector3 targetPos = Vector3.zero;

    private IEnumerator SetDestination()
    {
        for(int i = 0; i <4; i++)
        {
            float time = 0f;
            targetPos = Camera.main.WorldToScreenPoint(_target.transform.position);
            targetPos.y *= -1f;
            targetPos.y += Screen.currentResolution.height;
            Vector2 mousePosition = (Vector2)Input.mousePosition;
            mousePosition.y *= -1f;
            mousePosition.y += Screen.currentResolution.height;
            while (time < 1f)
            {
                SetCursorPos((int)Mathf.Lerp(mousePosition.x, targetPos.x, time), (int)Mathf.Lerp(mousePosition.y , targetPos.y, time));// (int)targetPos.y);
                Debug.Log(((int)targetPos.x).ToString() + "fasdf" + ((int)targetPos.y).ToString());
                time += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}