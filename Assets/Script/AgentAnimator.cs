using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimator : MonoBehaviour
{
    private Animator _animator = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void MoveAnimation(float amount)
    {
        if(Mathf.Abs(amount) > 0f)
        {
            _animator.SetBool("Move", true);
        }
        else
        {
            _animator.SetBool("Move",false);
        }
    }

    public void JumpAnimation(float amount)
    {
        _animator.SetFloat("MoveY", amount);
    }
    public void IsGrounded(bool value)
    {
        _animator.SetBool("IsGround", value);
    }

    public void AttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }

    public void DashAnimation()
    {
        _animator.SetTrigger("Dash");
    }

    public void AnimationReset()
    {
        _animator.SetBool("Dash", false);
        _animator.SetBool("Move", false);
        _animator.SetBool("IsGround", true);
    }
}
