using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class Save : MonoBehaviour
{
    [SerializeField]
    private bool _isTest = false;
    [SerializeField]
    private Transform _currentSavePoint = null;
    [SerializeField]
    private GameObject _player = null;
    private PlayerMovement _playerMovement;

    public PlayerMovement playerMovemant
    {
        get
        {
            if (_playerMovement == null)
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


    private float _playTime = 0f;
    private float _firstPlayTime = 0f;
    private int _deathCount = 0;
    public int DeathCount
    {
        get => _deathCount;
        set => _deathCount = value;
    }

    private SaveDatas _lastSave;

    private RealPlayer _realPlayer = null;

    private void Awake()
    {
        _instance = this;

        if (_isTest == false)
            LoadData();

        _rigid = _player.GetComponent<Rigidbody2D>();
        _saveable = true;
    }

    private void Start()
    {
        _realPlayer = playerMovemant.GetComponent<RealPlayer>();

        Restart();
        OnFirstSave?.Invoke();
    }

    private void Update()
    {
        _playTime += Time.deltaTime;
    }

    public Vector3 CurrentSavePoint
    {
        get => _currentSavePoint.position;
    }

    public void SetCurrentMap(Map currentMap)
    {
        _currentMap = currentMap;


        _realPlayer.OnMapChanged?.Invoke();
        PlayerLightSet();
    }

    public void PlayerLightSet()
    {
        string[] mapClamp = _currentMap.name.Split("Map_");
        if (mapClamp[0] == "3")
        {
            _realPlayer.LightDown();
        }
        else
        {
            _realPlayer.LightUp();
        }
    }

    public void SavePointSet(Map saveMap)
    {
        _lastSave.map = _saveMap;
        _lastSave.position = _currentSavePoint.position;

        _saveMap = saveMap;
        _currentSavePoint.position = _player.transform.position;

        OnSave?.Invoke();
        SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    /// <summary>
    /// 현재 맵이 SaveMap과 다르다면 true 반환
    /// </summary>
    /// <returns></returns>
    public bool IsDeffrentMapFromSaveMap()
    {
        if (_saveMap == null || _currentMap == null) return true;
        if (_saveMap != _currentMap) return true;
        return false;
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("SAVE_MAP", $"{_saveMap.name}");
        PlayerPrefs.SetFloat("SAVE_POINT_X", transform.position.x);
        PlayerPrefs.SetFloat("SAVE_POINT_Y", transform.position.y);
        PlayerPrefs.SetString("SAVE_DIFFICULTY", $"{DifficultyManager.Instance.difficulty.ToString()}");
        PlayerPrefs.SetFloat("SAVE_PLAYTIME", _playTime + _firstPlayTime);
        PlayerPrefs.SetInt("SAVE_DEATHCOUNT", _deathCount);
    }

    private void LoadData()
    {
        _firstPlayTime = PlayerPrefs.GetFloat("SAVE_PLAYTIME", 0f);
        _deathCount = PlayerPrefs.GetInt("SAVE_DEATHCOUNT", 0);

        Difficulty difficulty = Enum.Parse<Difficulty>(PlayerPrefs.GetString("SAVE_DIFFICULTY", "Normal"));
        DifficultyManager.Instance.difficulty = difficulty;

        string[] mapData = null;
        mapData = PlayerPrefs.GetString("SAVE_MAP", $"1Map_0").Split("Map_");
        string stage = mapData[0];
        string parentName = "Stage" + stage;

        _saveMap = GameObject.Find(parentName).transform.Find(PlayerPrefs.GetString("SAVE_MAP", "1Map_0")).GetComponent<Map>();
        _currentMap = _saveMap;
        Vector3 pos = new Vector3(PlayerPrefs.GetFloat("SAVE_POINT_X", -8.21f), PlayerPrefs.GetFloat("SAVE_POINT_Y", -3.23f), 0f);
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

        _warringText?.WarringReset();
        if (CameraManager.instance != null)
            CameraManager.instance.CompletePrevFeedBack();

        PlayerLightSet();
    }

    public void RevertSave()
    {
        if (_lastSave.map == null) return;
        Map tempMap = _saveMap;
        Vector3 tempPos = _currentSavePoint.position;

        _saveMap = _lastSave.map;
        _currentSavePoint.position = _lastSave.position;

        _lastSave.map = tempMap;
        _lastSave.position = tempPos;
        Restart();
    }


    [SerializeField]
    private WarringText _warringText = null;

    public void Warring(string text)
    {
        _warringText.Warring(text);
    }
}

public struct SaveDatas
{
    public Map map;
    public Vector3 position;
}