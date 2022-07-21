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
        Debug.Log("���Ϥ� 1");
        yield return new WaitForSeconds(2f);
        Debug.Log("���Ϥ� 2");
        yield return new WaitForSeconds(2f);
        Debug.Log("���Ϥ� 3");
        yield return new WaitForSeconds(2f);
        Debug.Log("���Ϥ� 4");
        yield return new WaitForSeconds(2f);
        Debug.Log("���� 2");
        yield return new WaitForSeconds(2f);
        Debug.Log("���� 3");
        yield return new WaitForSeconds(2f);
        Debug.Log("���� 4");
    }

    public void ResetBoss()
    {
        StopAllCoroutines();
        transform.position = Vector3.zero;
        Debug.Log("���¶�");
    }
}
