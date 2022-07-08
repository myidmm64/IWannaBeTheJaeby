using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    [SerializeField]
    private Transform _currentSavePoint = null;
    [SerializeField]
    private GameObject _player = null;
    [SerializeField]
    private StageManager _stageManager = null;

    private static Save _instance = null;
    public static Save Instance
    {
        get => _instance;
    }

    private Rigidbody2D _rigid = null;

    private void Awake()
    {
        _instance = this;
        _rigid = _player.GetComponent<Rigidbody2D>();
    }

    public Vector3 CurrentSavePoint
    {
        get => _currentSavePoint.position;
    }


    public void SavePointSet()
    {
        _currentSavePoint.position = _player.transform.position;
    }

    public void Restart()
    {
        _player.SetActive(true);
        _rigid.velocity = Vector2.zero;
        _player.transform.position = _currentSavePoint.position;
    }

}
