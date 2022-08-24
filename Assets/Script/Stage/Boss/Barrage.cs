using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage : BulletMove
{
    private BoxCollider2D _col = null;
    private SpriteRenderer _sprite = null;
    private Vector2 _size = Vector3.zero;
    private Vector2 _offset = Vector2.zero;

    public Vector2 Size
    {
        get => _size;
        set
        {
            _size = value;
            _col.size = _size;
        }
    }
    public Vector2 Offset
    {
        get => _offset;
        set
        {
            _offset = value;
            _col.offset = _offset;
        }
    }

    protected override void Check(Collider2D collision)
    {
        base.Check(collision);
    }

    protected override void Awake()
    {
        base.Awake();

        _sprite = GetComponent<SpriteRenderer>();
        _col = GetComponent<BoxCollider2D>();
        _size = _col.bounds.size;
        _offset = _col.offset;
    }

    protected override void ChildReset()
    {
        _sprite.sprite = null;
        _col.size = Vector2.zero;
        _col.offset = Vector2.zero;
        _sprite.enabled = false;
        _col.enabled = false;
        transform.localScale = Vector3.one;
    }

    protected override void Move()
    {
        transform.Translate(Vector3.up * Speed * Time.deltaTime);
    }

    public void SetBarrage(float speed, Vector2 size, Vector2 offset, Sprite sprite)
    {
        Speed = speed;
        _sprite.sprite = sprite;
        Size = size;
        Offset = offset;

        _sprite.enabled = true;
        _col.enabled = true;
    }
}
