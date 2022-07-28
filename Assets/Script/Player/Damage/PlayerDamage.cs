using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDamage : AgentDamage
{
    [field:SerializeField]
    private UnityEvent OnDie = null;
    private PlayerJump _playerJump = null;
    [SerializeField]
    private TextMeshPro _playerVoice = null;

    [SerializeField]
    private ParticleSystem _dieParticle = null;

    protected override void Awake()
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
        }
    }
    public override void Die()
    {
        Debug.Log("ав╬З╬Н©Д");
        DieVoice();
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

    public void DieVoice()
    {
        _playerVoice.transform.SetParent(null);
        _playerVoice.enabled = true;
        switch (Random.Range(0, 5))
        {
            case 0:
                _playerVoice.SetText("Tlqkf!!");
                break;
            case 1:
                _playerVoice.SetText("Noooooo");
                break;
            case 2:
                _playerVoice.SetText("What the..");
                break;
            case 3:
                _playerVoice.SetText("Oh my..");
                break;
            case 4:
                _playerVoice.SetText("Critical!!");
                break;
            default:
                break;
        }
    }

    public void DieVoiceReset()
    {
        _playerVoice.transform.SetParent(transform.root);
        _playerVoice.enabled = false;
        _playerVoice.SetText("");
    }
}
