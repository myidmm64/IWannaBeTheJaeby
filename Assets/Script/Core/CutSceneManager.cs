using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _text = null;
    [SerializeField]
    private string[] _texts = null;
    private int index = 0;

    public void SetText()
    {
        _text.SetText(_texts[index]);
        index++;
    }

}
