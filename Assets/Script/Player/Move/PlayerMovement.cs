using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : AgentMovement
{
    [SerializeField]
    private Transform _visualSpriteTrm = null;

    private bool _filp = false;

    protected override void Update()
    {
        base.Update();

        _moveDir.x = Input.GetAxisRaw("Horizontal");
        _moveDir.y = _rigid.velocity.y;

        if(_rigid.velocity.y <= -5f)
        {
            _moveDir = new Vector2(_moveDir.x, -5f);
        }

        Debug.Log(_moveDir.y);


        if(Input.GetKeyDown(KeyCode.Z))
        {
            _filp = _visualSpriteTrm.localScale.x < 0f;

            if(_filp)
            {
                Dash(Vector2.left);
            }
            else
            {
                Dash(Vector2.right);
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
    }
}
