using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPlayer : MonoBehaviour
{
    [SerializeField]
    private RectTransform _startPos = null;
    [SerializeField]
    private RectTransform _endPos = null;
    [SerializeField]
    private Slider _loadingSlider = null;

    private RectTransform _trm = null;

    private void Awake()
    {
        _trm = GetComponent<RectTransform>();
    }

    private void Update()
    {
        _trm.position = Vector3.Lerp(_startPos.position, _endPos.position, _loadingSlider.value);
    }
}
