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

    protected override void Update()
    {
        base.Update();

        _moveDir.x = Input.GetAxisRaw("Horizontal");
        _moveDir.y = _rigid.velocity.y;


        if(Input.GetKeyDown(KeyCode.Z))
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
