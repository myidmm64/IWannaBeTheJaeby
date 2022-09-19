using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacheSprite : MonoBehaviour
{
    private Transform _breathTrm = null;
    [SerializeField]
    private GameObject _breathObject = null;
    private List<GameObject> _breaths = new List<GameObject>();

    private void Start()
    {
        _breathTrm = transform.Find("BreathPosition");
    }

    public void SpawnBreath()
    {
        GameObject breath = Instantiate(_breathObject, _breathTrm);
        _breaths.Add(breath);
    }

    public void DetachBreath()
    {
        if(_breaths.Count > 0)
        {
            for(int i = 0; i < _breaths.Count; i++)
            {
                Destroy(_breaths[i]);
            }
            _breaths.Clear();
        }
    }

    private void OnDisable()
    {
        DetachBreath();
    }
}
