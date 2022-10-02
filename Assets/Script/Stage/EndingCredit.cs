using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class EndingCredit : MonoBehaviour
{
    [SerializeField]
    private RectTransform _target = null;
    [SerializeField]
    private RectTransform _trm = null;
    [SerializeField]
    private float _duration = 5f;

    [SerializeField]
    private BackgroundMove _backgroundMove = null;
    [SerializeField]
    private Animator _animator = null;
    private Sequence seq = null;

    private void Start()
    {
        StartEnding();
        StartCoroutine(EndingAnimationCoroutine());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (seq != null)
                seq.Kill();
            StopAllCoroutines();
            SceneManager.LoadScene(0);
        }
    }

    private IEnumerator EndingAnimationCoroutine()
    {
        yield return new WaitForSeconds(2f);
        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(3f);
        _animator.SetTrigger("Jump");
        yield return new WaitForSeconds(0.5f);
        _backgroundMove.enabled = true;
    }

    public void StartEnding()
    {
        seq = DOTween.Sequence();
        seq.Append(_trm.DOMove(_target.position, _duration));
        seq.AppendCallback(() =>
        {
            SceneManager.LoadScene(0);
            seq.Kill();
        });

    }
}
