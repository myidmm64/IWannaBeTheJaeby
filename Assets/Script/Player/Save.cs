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
    private PlayerMovement _playerMovement;

    public PlayerMovement playerMovemant
    {
        get
        {
            if(_playerMovement == null)
            {
                _playerMovement = _player.GetComponent<PlayerMovement>();
            }
            return _playerMovement;
        }
    }

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

    [field: SerializeField]
    private UnityEvent OnFirstSave = null;

    private void Awake()
    {
        _instance = this;
        _rigid = _player.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Restart();
        OnFirstSave?.Invoke();
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
        _currentMap.transform.root.gameObject.SetActive(false);

        _player.SetActive(true);
        _rigid.velocity = Vector2.zero;
        _player.transform.position = _currentSavePoint.position;

        _saveMap.gameObject.SetActive(true);
        _saveMap.transform.root.gameObject.SetActive(true);
        _saveMap.transform.root.GetComponent<StageBGMAudio>().NormalBGMPlay();
        _saveMap.Init();

        _currentMap = _saveMap;
    }

}
