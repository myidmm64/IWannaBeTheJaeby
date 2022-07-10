using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChanger : MonoBehaviour
{
    [SerializeField]
    private Map _currentMap = null;
    [SerializeField]
    private Map _nextMap = null;
    [SerializeField]
    private Transform _nextPosition = null;
    [SerializeField]
    private bool _changeMap = true;

    [Header("���� ���������� �Ѿ ����")]
    [SerializeField]
    private bool _changeStage = false;
    [SerializeField]
    private GameObject _CurrentStage = null;
    [SerializeField]
    private GameObject _NextStage = null;


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
        Debug.Log("���� �� !!");


        player.transform.position = _nextPosition.position;

        if (_changeStage == true)
        {
            _nextMap.gameObject.SetActive(true);
            _nextMap.Init();
            _NextStage.gameObject.SetActive(true);
            _NextStage.GetComponent<StageBGMAudio>().NormalBGMPlay();
            Save.Instance.SetCurrentMap(_nextMap);
            _CurrentStage.gameObject.SetActive(false);
            _currentMap.gameObject.SetActive(false);

            return;
        }

        if (_changeMap == true)
        {
            _nextMap.gameObject.SetActive(true);
            _nextMap.Init();

            Save.Instance.SetCurrentMap(_nextMap);
            _currentMap.gameObject.SetActive(false);
        }


    }
}
