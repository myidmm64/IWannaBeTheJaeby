using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController _instance;
    public static TimeController instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameObject("TimeController").AddComponent<TimeController>();
            return _instance;
        }
    }

    public void ResetTimeScale()
    {
        StopAllCoroutines();
        Time.timeScale = 1f;
    }

    /// <summary>
    /// �ð��� ���ߴ� �Լ�
    /// </summary>
    /// <param name="�����ų Ÿ�ӽ������� ��"></param>
    /// <param name="��ٸ��� �ð�"></param>
    /// <param name="������ �� �����ų �׼�"></param>
    public void ModifyTimeScale(float endTimeValue, float waitTime, Action OnComplateAction = null)
    {
        StartCoroutine(TimeScaleCoroutine(endTimeValue, waitTime, OnComplateAction));
    }

    IEnumerator TimeScaleCoroutine(float endTimeValue, float waitTime, Action OnComplateAction = null)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        Time.timeScale = endTimeValue;
        OnComplateAction?.Invoke();
    }
}
