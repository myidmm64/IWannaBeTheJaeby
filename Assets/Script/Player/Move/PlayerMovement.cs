using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : AgentMovement
{
    [SerializeField]
    private Transform _visualSpriteTrm = null;

    [SerializeField]
    private float _moreDashCooltime = 0.4f;
    private Coroutine _moreDashCoroutine = null;

    private bool _filp = false;

    KeySetting _keySetting = null;

    private void Start()
    {
        if(_keySetting == null)
        {
            _keySetting = KeyManager.Instance.keySetting;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKey(_keySetting.Keys[KeyAction.RIGHT]))
        {
            _moveDir.x = 1f;
            if (Input.GetKey(_keySetting.Keys[KeyAction.LEFT]))
            {
                _moveDir.x = 0f;
            }
        }
        else if (Input.GetKey(_keySetting.Keys[KeyAction.LEFT]))
        {
            _moveDir.x = -1f;
            if (Input.GetKey(_keySetting.Keys[KeyAction.RIGHT]))
            {
                _moveDir.x = 0f;
            }
        }

        else
        {
            _moveDir.x = 0f;
        }

        _moveDir.y = _rigid.velocity.y;


        if(Input.GetKeyDown(_keySetting.Keys[KeyAction.DASH]))
        {
            if(_dashable || _moreDash)
            {
                _filp = _visualSpriteTrm.localScale.x < 0f;

                if (_filp)
                {
                    Dash(Vector2.left);
                }
                else
                {
                    Dash(Vector2.right);
                }
            }

        }
    }

    public void DashEnable()
    {
        _dashable = true;
    }
    public void DashDisable()
    {
        _dashable = false;
        _moreDash = false;
    }

    public void MoreDash()
    {
        if (_moreDashCoroutine != null)
            StopCoroutine(_moreDashCoroutine);
        _moreDashCoroutine = StartCoroutine(MoreDashCoroutine());
    }
    private IEnumerator MoreDashCoroutine()
    {
        _moreDash = true;
        yield return new WaitForSeconds(_moreDashCooltime);
        _moreDash = false;
    }
}
