using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{

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
            Save.Instance.SavePointSet();
        }
    }
}
