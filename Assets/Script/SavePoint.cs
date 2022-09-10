using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SavePoint : MonoBehaviour
{
    [SerializeField]
    private Map _saveMap = null;
    private Animator _animator = null;
    private Collider2D _col = null;

    private void Awake()
    {
        _col = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        _col.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerAtk"))
        {
            Debug.Log("ºº¿Ã∫Í");
            _animator.SetTrigger("Save");
            StartCoroutine(SaveCoroutine());
            PaticleObj p = PoolManager.Instance.Pop("SaveParticle") as PaticleObj;
            p.transform.position = transform.position;
            Save.Instance.SavePointSet(_saveMap);
        }
    }

    private IEnumerator SaveCoroutine()
    {
        _col.enabled = false;
        yield return new WaitForSeconds(0.5f);
        _col.enabled = true;
    }
}
