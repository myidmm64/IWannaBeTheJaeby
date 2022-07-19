using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentDamage : MonoBehaviour
{
    [SerializeField]
    private int _hp = 1;
    [SerializeField]
    private bool _isEnemy = false;

    private SpriteRenderer _spriteRenderer = null;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(_isEnemy == false)
        {
            if (collision.CompareTag("Die"))
            {
                Damaged();
            }
            if (collision.CompareTag("Enemy"))
            {
                Damaged();
            }

        }
        else
        {
            if (collision.CompareTag("Player"))
            {
                Damaged();
            }

            if (collision.CompareTag("PlayerAtk"))
            {
                Damaged();
            }
        }

    }

    public abstract void Die();

    private void Damaged()
    {
        _hp--;

        if (_hp <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(DamageCoroutine());
    }

    private IEnumerator DamageCoroutine()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.white;
    }
}
