using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDamage : AgentDamage
{
    [field:SerializeField]
    private UnityEvent OnDie = null;
    private PlayerJump _playerJump = null;

    [SerializeField]
    private ParticleSystem _dieParticle = null;

    private void Awake()
    {
        _playerJump = transform.parent.GetComponent<PlayerJump>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.CompareTag("MoreJump"))
        {
            _playerJump.MoreJump();
        }
        if (collision.CompareTag("Interaction"))
        {
            Debug.Log("Interaction");
        }
    }
    public override void Die()
    {
        Debug.Log("ав╬З╬Н©Д");
        
        OnDie?.Invoke();
        transform.root.gameObject.SetActive(false);
    }

    public void DieEffectEnable()
    {
        _dieParticle.gameObject.SetActive(true);
        _dieParticle.transform.position = transform.position;
        _dieParticle.Play();
    }
    public void DieEffectDisable()
    {
        _dieParticle.Stop();
        _dieParticle.gameObject.SetActive(false) ;
    }
}
