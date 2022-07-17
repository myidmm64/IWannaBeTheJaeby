using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TorchAnimation : MonoBehaviour
{
    [SerializeField]
    private bool _changeRadius = true; // ��鸱 �� �ݰ���� ��??

    [SerializeField] private float _intensityRandomness;
    [SerializeField] private float _radiusRandomness;
    [SerializeField] private float _timeRandomness;

    private float _baseIntensity;
    private float _baseTime = 0.5f;
    private float _baseRadius;

    private UnityEngine.Rendering.Universal.Light2D _light;
    private Sequence seq = null;
    private void Awake()
    {
        _light = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
        _baseIntensity = _light.intensity;
        _baseRadius = _light.pointLightOuterRadius;
    }
    private void OnEnable()
    {
        ShakeLight();
    }
    private void OnDestroy()
    {
        seq?.Kill();
    }
    private void ShakeLight()
    {
        //��Ʈ�� ������ ��鸮
        //������ �������� �ٽ��ѹ� ȣ��
        float targetIntensity = _baseIntensity + Random.Range(-_intensityRandomness, _intensityRandomness);
        float targetTime = _baseTime + Random.Range(-_timeRandomness, _timeRandomness);
        float targetRadius = _baseRadius + Random.Range(-_radiusRandomness, _radiusRandomness);

        if (!gameObject.activeSelf)
            return;

        seq = DOTween.Sequence();
        seq.Append(DOTween.To(
            () => _light.intensity,
            value => _light.intensity = value,
            targetIntensity,
            targetTime
        ));
        //Debug.Log(targetIntensity);
        //������ ���� ~
        if (_changeRadius)
        {
            seq.Join(DOTween.To(
            () => _light.pointLightOuterRadius,
            value => _light.pointLightOuterRadius = value,
            targetRadius,
            targetTime
        ));
        }


        seq.AppendCallback(() => ShakeLight());
    }
}
