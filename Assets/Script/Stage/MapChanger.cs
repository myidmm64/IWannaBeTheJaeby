using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChanger : MonoBehaviour
{
    [SerializeField]
    private Map _currentStage = null;
    [SerializeField]
    private Map _nextStage = null;
    [SerializeField]
    private Transform _nextPosition = null;


    private void Awake()
    {
        _nextPosition = transform.Find("NextPosition");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            ChangeMap(collision.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangeMap(collision.gameObject);
        }
    }

    public void ChangeMap(GameObject player)
    {
        Debug.Log("¥Ÿ¿Ω ∏  !!");
        _nextStage.gameObject.SetActive(true);
        player.transform.position = _nextPosition.position;
        _currentStage.gameObject.SetActive(false);
    }
}
