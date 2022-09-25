using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class RealPlayer : MonoBehaviour
{
    public UnityEvent OnMapChanged = null;
    [SerializeField]
    private Light2D _light = null;

    [SerializeField]
    private float _lightDownIntensity = 0.25f;
    [SerializeField]
    private float _lightUpIntensity = 0.7f;

    public void LightDown()
    {
        _light.intensity = _lightDownIntensity;
    }
    public void LightUp()
    {
        _light.intensity = _lightUpIntensity;
    }
}
