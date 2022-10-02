using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class SmallFrog : MonoBehaviour
{
    private Vector3 _originPos = Vector3.zero;
    [SerializeField]
    private List<Transform> _landingPositions = new List<Transform>();
    [SerializeField]
    private AudioClip _landingClip = null;
    private Sequence _seq = null;
    [field: SerializeField]
    private UnityEvent OnJumpEnd = null;


    private void Start()
    {
        _originPos = transform.position;
    }

    public void StartFrogJump()
    {
        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();
        for(int i = 0; i  < _landingPositions.Count; i++)
        {
            _seq.AppendInterval(0.2f);
            _seq.Append(transform.DOMove(_landingPositions[i].position + Vector3.up * 4f, 0.4f));
            _seq.AppendInterval(0.2f);
            _seq.Append(transform.DOMoveY(_landingPositions[i].position.y, 0.15f));
            _seq.AppendCallback(() =>
            {
                CameraManager.instance.CameraShake(12f, 30f, 0.2f);
                AudioPoolable a = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
                a.Play(_landingClip);
            });
        }
        _seq.AppendCallback(() =>
        {
            transform.position = _originPos;
            OnJumpEnd?.Invoke();
            _seq.Kill();
        });
    }

    private void OnDisable()
    {
        if (_seq != null)
            _seq.Kill();
        transform.position = _originPos;
    }
}
