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
    /// 시간을 멈추는 함수
    /// </summary>
    /// <param name="변경시킬 타임스케일의 양"></param>
    /// <param name="기다리는 시간"></param>
    /// <param name="끝났을 때 실행시킬 액션"></param>
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
