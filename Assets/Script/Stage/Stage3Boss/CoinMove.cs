using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : AgentJump
{
    [SerializeField]
    private float _speed = -3f;
    private Vector3 _originPos = Vector3.zero;
    [SerializeField]
    private AudioClip _breakClip = null;
    private AudioSource _audioSource = null;

    private void FixedUpdate()
    {
        _rigid.velocity = new Vector2(_speed ,_rigid.velocity.y);
    }

    private void OnEnable()
    {
        if(_audioSource == null)
            _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
        _originPos = transform.position;
    }

    protected override void Update()
    {
        base.Update();
        if(_isground)
        {
            ForceJump(Random.Range(_jumpPower * 0.9f, _jumpPower), Vector3.up);

            _audioSource.Play();
            //AudioPoolable audio = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
            //audio.PlayRandomness(_breakClip);
            CameraManager.instance.CameraShake(4f, 20f, 0.2f);
        }
    }

    private void OnDisable()
    {
        transform.position = _originPos;
    }
}
