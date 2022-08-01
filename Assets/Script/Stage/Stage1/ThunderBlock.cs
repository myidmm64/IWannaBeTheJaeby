using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBlock : MonoBehaviour
{
    [SerializeField]
    private float _interval = 2f;
    [SerializeField]
    private AudioClip _thunderClip = null;

    private Animator _animator = null;
    private SpriteRenderer _spriteRenderer = null;

    private void OnEnable()
    {
        if(_animator == null)
        {
            _animator = transform.Find("Thunder").GetComponent<Animator>();
            _spriteRenderer = _animator.GetComponent<SpriteRenderer>();
        }
        StopCoroutine("ThunderSpawn");
        StartCoroutine(ThunderSpawn());
    }

    private IEnumerator ThunderSpawn()
    {
        _spriteRenderer.enabled = false;
        while (true)
        {
            yield return new WaitForSeconds(_interval);
            _spriteRenderer.enabled = true;
            _animator.SetTrigger("Thunder");
            AudioPoolable a = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
            a.Play(_thunderClip);
        }
    }
}
