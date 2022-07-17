using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Playables;

public class CutSceneManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _text = null;
    [SerializeField]
    private string[] _texts = null;
    private int index = 0;

    [Header("¸Ç Ã³À½")]
    [SerializeField]
    private GameObject _player = null;

    private bool _isFirstStart = true;
    [SerializeField]
    private GameObject[] StartCutSceneObjects = null;
    [SerializeField]
    PlayableDirector _director = null;
    [SerializeField]
    private GameObject[] _firstCutSceneEnable = null;

    public void CutSceneInit()
    {
        
        if(_isFirstStart)
        {
            FirstStart();
            _isFirstStart = false;
        }
        else
        {
            for (int i = 0; i < StartCutSceneObjects.Length; i++)
            {
                StartCutSceneObjects[i].SetActive(false);
            }
            _player.SetActive(true);
        }
    }

    public void SetText()
    {
        _text.SetText(_texts[index]);
        index++;
    }

    public void FirstStart()
    {
        StartCoroutine(StartCutScene());
    }
    private IEnumerator StartCutScene()
    {
        _player.SetActive(false);
        for (int i = 0; i < StartCutSceneObjects.Length; i++)
        {
            StartCutSceneObjects[i].SetActive(true);
        }
        _director.Play();
        yield return new WaitForSeconds(12.5f);
        for (int i = 0; i < StartCutSceneObjects.Length; i++)
        {
            StartCutSceneObjects[i].SetActive(false);
        }
        for(int i = 0; i<_firstCutSceneEnable.Length; i++)
        {
            _firstCutSceneEnable[i].SetActive(true);
        }
        _player.SetActive(true);
    }

}
