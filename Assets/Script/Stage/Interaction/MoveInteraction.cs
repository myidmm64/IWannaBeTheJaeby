using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class MoveInteraction : Interaction
{
    [SerializeField]
    private MoveTarget[] _moveTargets;

    private bool _isFirst = true;

    private void OnEnable()
    {
        TargetsReset();
    }

    public override void DoEnterInteraction()
    {
        for (int i = 0; i<_moveTargets.Length; i++)
        {
            Vector2 dir = Vector2.zero;

            switch(_moveTargets[i].dir)
            {
                case Direction.LEFT_TOP:
                    dir = new Vector2(-1f,1f);
                    break;
                case Direction.LEFT_SIDE:
                    dir = new Vector2(-1f, 0f);
                    break;
                case Direction.LEFT_BOTTOM:
                    dir = new Vector2(-1f, -1f);
                    break;
                case Direction.RIGHT_TOP:
                    dir = new Vector2(1f, 1f);
                    break;
                case Direction.RIGHT_SIDE:
                    dir = new Vector2(1f, 0f);
                    break;
                case Direction.RIGHT_BOTTOM:
                    dir = new Vector2(1f, -1f);
                    break;
                case Direction.MIDDLE_TOP:
                    dir = new Vector2(0f, 1f);
                    break;
                case Direction.MIDDLE_BOTTOM:
                    dir = new Vector2(0f, -1f);
                    break;
                default:
                    break;
            }

            _moveTargets[i].target.gameObject.SetActive(true);
            _moveTargets[i].target.GetComponent<SpriteRenderer>().enabled = true;
            _moveTargets[i].target.GetComponent<Collider2D>().enabled = true;

            _moveTargets[i].target.velocity = dir * _moveTargets[i].speed;
        }
    }

    public override void DoExitInteraction()
    {
    }

    public override void DoStayInteraction()
    {
    }

    public void TargetsReset()
    {
        if (_isFirst)
        {
            _isFirst = false;
            for (int i = 0; i < _moveTargets.Length; i++)
            {
                _moveTargets[i].originPos = _moveTargets[i].target.position;
                Debug.Log(_moveTargets[i].originPos);
            }
        }

        for (int i = 0; i<_moveTargets.Length; i++)
        {
            _moveTargets[i].target.position = (Vector3)_moveTargets[i].originPos;
            _moveTargets[i].target.velocity = Vector2.zero;

            if (_moveTargets[i].disable)
            {
                //_moveTargets[i].target.gameObject.SetActive(false);
                _moveTargets[i].target.GetComponent<SpriteRenderer>().enabled = false;
                _moveTargets[i].target.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}

[Serializable]
public struct MoveTarget
{
    public Rigidbody2D target;
    public Vector2 originPos;
    public float speed;
    public Direction dir;
    public bool disable;
}

[Serializable]
public enum Direction
{
    LEFT_TOP,
    LEFT_SIDE,
    LEFT_BOTTOM,
    RIGHT_TOP,
    RIGHT_SIDE,
    RIGHT_BOTTOM,
    MIDDLE_TOP,
    MIDDLE_BOTTOM
}
