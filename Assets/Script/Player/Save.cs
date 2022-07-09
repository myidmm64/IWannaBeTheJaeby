using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Save : MonoBehaviour
{
    [SerializeField]
    private Transform _currentSavePoint = null;
    [SerializeField]
    private GameObject _player = null;

    [SerializeField]
    private Map _saveMap = null;
    [SerializeField]
    private Map _currentMap = null;

    [field: SerializeField]
    private UnityEvent OnSave = null;

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

    private void Start()
    {
        Restart();
    }

    public Vector3 CurrentSavePoint
    {
        get => _currentSavePoint.position;
    }

    public void SetCurrentMap(Map currentMap)
    {
        _currentMap = currentMap;
    }

    public void SavePointSet(Map saveMap)
    {
        _saveMap = saveMap;

        _currentSavePoint.position = _player.transform.position;

        OnSave?.Invoke();
    }

    public void Restart()
    {
        _currentMap.gameObject.SetActive(false);
        _saveMap.gameObject.SetActive(true);
        _saveMap.transform.root.GetComponent<StageBGMAudio>().NormalBGMPlay();

        _player.SetActive(true);
        _rigid.velocity = Vector2.zero;
        _player.transform.position = _currentSavePoint.position;

        _currentMap = _saveMap;
    }

}
