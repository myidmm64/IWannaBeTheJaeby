using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SavePoint : MonoBehaviour
{
    [SerializeField]
    private Map _saveMap = null;
    [field: SerializeField]
    private UnityEvent OnSave = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerAtk"))
        {
            Debug.Log("ºº¿Ã∫Í");
            Save.Instance.SavePointSet(_saveMap);
            OnSave?.Invoke();
        }
    }
}
