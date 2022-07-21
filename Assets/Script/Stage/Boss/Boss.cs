using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public void StartBoss()
    {
        StartCoroutine(RRR());
    }

    private void OnDisable()
    {
        ResetBoss();
    }

    private IEnumerator RRR()
    {
        Debug.Log("패턴ㄴ 1");
        yield return new WaitForSeconds(2f);
        Debug.Log("패턴ㄴ 2");
        yield return new WaitForSeconds(2f);
        Debug.Log("패턴ㄴ 3");
        yield return new WaitForSeconds(2f);
        Debug.Log("패턴ㄴ 4");
        yield return new WaitForSeconds(2f);
        Debug.Log("패턴 2");
        yield return new WaitForSeconds(2f);
        Debug.Log("패턴 3");
        yield return new WaitForSeconds(2f);
        Debug.Log("패턴 4");
    }

    public void ResetBoss()
    {
        StopAllCoroutines();
        transform.position = Vector3.zero;
        Debug.Log("리셋또");
    }
}
