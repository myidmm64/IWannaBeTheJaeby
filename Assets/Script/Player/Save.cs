using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    private bool _saveable = true;
    public bool Saveable
    {
        get => _saveable;
        set => _saveable = value;
    }


    private void Awake()
    {
        _instance = this;
        LoadData();

        _rigid = _player.GetComponent<Rigidbody2D>();
        _saveable = true;
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
        SaveData();
    }

    private void SaveData()
    {
        PlayerPrefs.SetString("SAVE_MAP", $"{_saveMap.name}");
        PlayerPrefs.SetFloat("SAVE_POINT_X", transform.position.x);
        PlayerPrefs.SetFloat("SAVE_POINT_Y", transform.position.y);
        PlayerPrefs.SetString("SAVE_DIFFICULTY", $"{DifficultyManager.Instance.difficulty.ToString()}");
    }

    private void LoadData()
    {
        Difficulty difficulty = Enum.Parse<Difficulty>(PlayerPrefs.GetString("SAVE_DIFFICULTY", "Easy"));
        DifficultyManager.Instance.difficulty = difficulty;

        string[] mapData = null;
        mapData = PlayerPrefs.GetString("SAVE_MAP", $"1Map_0").Split("Map_");
        string stage = mapData[0];
        string parentName = "Stage" + stage;

        _saveMap = GameObject.Find(parentName).transform.Find(PlayerPrefs.GetString("SAVE_MAP", "1Map_0")).GetComponent<Map>();
        _currentMap = _saveMap;
        Vector3 pos = new Vector3(PlayerPrefs.GetFloat("SAVE_POINT_X", 0f), PlayerPrefs.GetFloat("SAVE_POINT_Y", 0f), 0f);
        transform.position = pos;

    }

    public void Restart()
    {
        if (_saveable == false) return;

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
