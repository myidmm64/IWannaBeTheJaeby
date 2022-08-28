using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinMove : AgentJump
{
    [SerializeField]
    private float _speed = -3f;
    private Vector3 _originPos = Vector3.zero;
    [SerializeField]
    private AudioClip _breakClip = null;
    private AudioSource _audioSource = null;
    [field: SerializeField]
    private UnityEvent OnJumpEnd = null;

    private void FixedUpdate()
    {
        _rigid.velocity = new Vector2(_speed ,_rigid.velocity.y);
    }

    private void OnEnable()
    {
        if(_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        _originPos = transform.position;
    }

    protected override void Update()
    {
        base.Update();
        if(_isground)
        {
            ForceJump(Random.Range(_jumpPower * 0.9f, _jumpPower), Vector3.up);

            if(_audioSource.clip != null)
                _audioSource.Play();
            //AudioPoolable audio = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
            //audio.PlayRandomness(_breakClip);
            CameraManager.instance.CameraShake(4f, 20f, 0.2f);
            OnJumpEnd?.Invoke();
        }
    }

    private void OnDisable()
    {
        transform.position = _originPos;
    }
}
