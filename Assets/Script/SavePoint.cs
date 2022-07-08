using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField]
    private Map _saveMap = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Save.Instance.SavePointSet(_saveMap);
        }
    }
}
