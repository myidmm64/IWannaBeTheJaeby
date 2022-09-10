using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaticleObj : PoolableMono
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public override void PopReset()
    {
    }

    public override void PushReset()
    {
    }

    private void OnParticleSystemStopped()
    {
        PoolManager.Instance.Push(this);
    }

    public void SetColor(Color color)
    {
        Debug.Log(color);
        ParticleSystem.MainModule module = _particleSystem.main;
        module.startColor = color;
        _particleSystem.Play();
    }
}
