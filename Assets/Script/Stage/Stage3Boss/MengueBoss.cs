using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MengueBoss : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer = null;
    private Collider2D _col = null;
    private void Awake()
    {
        _col = transform.Find("AgentSprite").GetComponent<Collider2D>();
        _spriteRenderer = _col.GetComponent<SpriteRenderer>();
    }

    public void BossReset()
    {
        if(_col != null)
        {
            _col.enabled = true;
            _spriteRenderer.enabled = true;
        }
    }
    public void DieReset()
    {
        if(_col != null)
        {
            _col.enabled = false;
        }
    }
}
