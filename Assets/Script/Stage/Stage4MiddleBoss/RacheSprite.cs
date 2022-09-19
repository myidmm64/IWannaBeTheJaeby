using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacheSprite : MonoBehaviour
{
    private Transform _breathTrm = null;
    [SerializeField]
    private GameObject _breathObject = null;
    private List<GameObject> _breaths = new List<GameObject>();

    private float _breathSize = 1f;
    public float BreathSize
    {
        get => _breathSize;
        set => _breathSize = value;
    }

    private bool _fliped = false;

    private Vector3 _originScale = Vector3.zero;

    private void Start()
    {
        _originScale = transform.localScale;
        _breathTrm = transform.Find("BreathPosition");
    }

    public void FlipSprite(bool flip)
    {
        Vector3 localScale = transform.localScale;
        if (flip)
        {
            localScale.x = Mathf.Abs(localScale.x) * -1f;
        }
        else
        {
            localScale.x = Mathf.Abs(localScale.x);
        }
        _fliped = flip;
        transform.localScale = localScale;
    }

    public void FlipReset()
    {
        transform.localScale = _originScale;
        _fliped = false;
    }

    public void SpawnBreath()
    {
        GameObject breath = Instantiate(_breathObject, _breathTrm);
        breath.transform.localScale = Vector3.one * BreathSize;
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
}
