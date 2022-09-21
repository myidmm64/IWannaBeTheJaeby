using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RacheSprite : MonoBehaviour
{
    private Transform _breathTrm = null;
    [SerializeField]
    private GameObject _breathObject = null;
    [SerializeField]
    private GameObject _bigBreathObject = null;
    [SerializeField]
    private GameObject _breathPositionObject = null;
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

    public void SpawnBigBreath()
    {
        GameObject breath = Instantiate(_bigBreathObject, _breathTrm);
        Transform target = breath.transform.Find("FireWall");
        Collider2D col = target.GetComponent<Collider2D>();
        col.enabled = false;
        target.transform.localScale = new Vector3(0.2f, target.transform.localScale.y, 1f);
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() =>
        {
            col.GetComponent<Collider2D>().enabled = true;
        });
        seq.Append(target.transform.DOScaleX(1.5f, 0.2f));
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

    public void EnableBreathPosition()
    {
        _breathPositionObject.SetActive(true);
    }

    public void DisableBreathPosition()
    {
        _breathPositionObject.SetActive(false);
    }

    private void OnDisable()
    {
        DisableBreathPosition();
    }
}
