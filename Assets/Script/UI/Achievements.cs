using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.IO;
using System.Text;

public class Achievements : MonoBehaviour
{
    private Vector2 _originAnchoredPos = Vector2.zero;
    private RectTransform _trm = null;
    [SerializeField]
    private RectTransform _endPosTrm = null;
    [SerializeField]
    private AudioClip _achiClip = null;
    [SerializeField]
    private TextMeshProUGUI _nameText = null;
    [SerializeField]
    private TextMeshProUGUI _explainText = null;
    private Vector2 _endPos = Vector2.zero;
    private Sequence _seq = null;

    private Dictionary<int, bool> _firstBossCheckDic = new Dictionary<int, bool>();
    private SaveableDicListData dicData = new SaveableDicListData();

    private void Start()
    {
        _trm = GetComponent<RectTransform>();
        _originAnchoredPos = _trm.anchoredPosition;
        _endPos = _endPosTrm.anchoredPosition;
        LoadAchieveData();
    }//

    [ContextMenu("ÆË¾÷")]
    public void Popup(string name, string explain, int key)
    {
        if (_firstBossCheckDic.ContainsKey(key) == false)
        {
            _firstBossCheckDic.Add(key, true);
            dicData._saveableDicList.Add(new SaveableDic
            {
                key = key,
                value = true
            });
        }
        if (_firstBossCheckDic[key] == false)
        {
            return;
        }

        if (_seq != null)
        {
            _seq.Kill();
            _trm.anchoredPosition = _originAnchoredPos;
        }
        _firstBossCheckDic[key] = false;
        for(int i = 0; i<dicData._saveableDicList.Count; i++)
        {
            if (dicData._saveableDicList[i].key == key)
            {
                dicData._saveableDicList[i] = new SaveableDic
                {
                    key = key,
                    value = false
                };
                break;
            }
        }

        PlayerPrefs.SetInt("SAVE_ACHIEVEMENT", PlayerPrefs.GetInt("SAVE_ACHIEVEMENT", 0) + 1);
        _nameText.SetText(name);
        _explainText.SetText(explain);

        _seq = DOTween.Sequence();
        _seq.Append(_trm.DOAnchorPos(_endPos, 1f));
        _seq.AppendCallback(() =>
        {
            AudioPoolable a = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
            a.Play(_achiClip);
        });
        _seq.AppendInterval(1.5f);
        _seq.Append(_trm.DOAnchorPos(_originAnchoredPos, 1f));
    }

    private void OnDestroy()
    {
        SaveAchieveData();
    }

    public void SaveAchieveData()
    {
        string path = Application.dataPath + "/Save/AchieveSave.json";
        string json = JsonUtility.ToJson(dicData);
        Debug.Log(json);
        File.WriteAllText(path, json);
    }

    public void LoadAchieveData()
    {
        string path = Application.dataPath + "/Save/AchieveSave.json";
        if (File.Exists(path) == false)
        {
            return;
        }
        string json = File.ReadAllText(path);
        if (json == "")
            return;

        dicData = JsonUtility.FromJson<SaveableDicListData>(json);
        for (int i = 0; i < dicData._saveableDicList.Count; i++)
        {
            _firstBossCheckDic.Add(dicData._saveableDicList[i].key, dicData._saveableDicList[i].value);
        }
    }
}

[System.Serializable]
public class SaveableDicListData
{
    public List<SaveableDic> _saveableDicList = new List<SaveableDic>();
}

[System.Serializable]
public struct SaveableDic
{
    public int key;
    public bool value;
}
