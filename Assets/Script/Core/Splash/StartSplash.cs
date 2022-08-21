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
}
