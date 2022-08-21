using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSplash : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Splash());
    }

    private IEnumerator Splash()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(3);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            StopAllCoroutines();
            DOTween.KillAll();

            SceneManager.LoadScene(3);
        }
    }
}
