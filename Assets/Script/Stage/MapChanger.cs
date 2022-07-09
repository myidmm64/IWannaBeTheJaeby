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
    [SerializeField]
    private bool _changeMap = true;


    private void Awake()
    {
        _nextPosition = transform.Find("NextPosition");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangeMap(collision.transform.root.gameObject);
        }
    }

    public void ChangeMap(GameObject player)
    {
        Debug.Log("¥Ÿ¿Ω ∏  !!");


        player.transform.position = _nextPosition.position;
        if (_changeMap == true)
        {
            _nextStage.gameObject.SetActive(true);
            _nextStage.Init();

            _currentStage.gameObject.SetActive(false);
            Save.Instance.SetCurrentMap(_nextStage);
        }

    }
}
