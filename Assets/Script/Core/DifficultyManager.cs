using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField]
    private Difficulty _difficulty = Difficulty.None;
    public Difficulty difficulty
    {
        get => _difficulty;
        set => _difficulty = value;
    }

    private static DifficultyManager _instance = null;
    public static DifficultyManager Instance => _instance;

    [field: SerializeField]
    private UnityEvent OnError = null;

    private void Awake()
    {
        _instance = this;
    }

    public void DifficultySet(Difficulty dif)
    {
        _difficulty = dif;
        PlayerPrefs.SetString("SAVE_DIFFICULTY", $"{DifficultyManager.Instance.difficulty.ToString()}");

        DontDestroyOnLoad(this);
        SceneManager.LoadScene(1);
    }

    public void ReStart()
    {
        if(PlayerPrefs.GetString("SAVE_DIFFICULTY") == "None" || PlayerPrefs.GetString("SAVE_DIFFICULTY") == "")
        {
            Debug.LogError("저장된 파일 없음 !!");
            OnError?.Invoke();
            return;
        }
        DontDestroyOnLoad(this);
        SceneManager.LoadScene(1);
    }

    [ContextMenu("이어하기 데이터 초기화")]
    public void DeleteSaveData()
    {
        PlayerPrefs.DeleteKey("SAVE_DIFFICULTY");
        PlayerPrefs.DeleteKey("SAVE_MAP");
        PlayerPrefs.DeleteKey("SAVE_POINT_X");
        PlayerPrefs.DeleteKey("SAVE_POINT_Y");
        PlayerPrefs.DeleteKey("SAVE_PLAYTIME");
        PlayerPrefs.DeleteKey("SAVE_DEATHCOUNT");
    }
}

[System.Serializable]
public enum Difficulty
{
    None,
    Easy,
    Normal,
    Hard,
    Extreme
}
