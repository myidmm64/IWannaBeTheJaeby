using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeDetector : MonoBehaviour
{
    [SerializeField]
    private Animator _animator = null;

    private void OnEnable()
    {
        AnimationReset();
    }

    public void DoInter()
    {
        _animator.SetBool("Fly", true);
    }
    public void AnimationReset()
    {
        _animator.SetBool("Fly", false);
    }
}
