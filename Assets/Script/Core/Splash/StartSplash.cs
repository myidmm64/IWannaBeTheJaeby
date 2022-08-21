using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSplash : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource = null;
    [SerializeField]
    private GameObject[] _fifreworks = null;
    [SerializeField]
    private BreakableWindow _window = null;
    [SerializeField]
    private SpriteRenderer _fadeImage = null;

    private void Start()
    {
        StartCoroutine(Splash());
    }

    private IEnumerator Splash()
    {
        yield return new WaitForSeconds(0.5f);
        _audioSource.Play();
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i <_fifreworks.Length; i++)
        {
            _fifreworks[i].SetActive(true);
        }

        yield return new WaitForSeconds(2f);
        for (int i = 0; i < _fifreworks.Length; i++)
        {
            _fifreworks[i].SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        CameraManager.instance.CameraShake(5f, 20f, 0.2f);
        _window.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        CameraManager.instance.CameraShake(10f, 40f, 0.3f);
        _window.breakWindow();

        yield return new WaitForSeconds(0.5f);
        _fadeImage?.DOFade(1f, 1f);
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
