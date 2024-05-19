using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class SmalSoldier : MonoBehaviour
{
    SoldierUIManager _soldierUIManager = null;
    [SerializeField]
    private Transform _bossObjectTrm = null;
    [SerializeField]
    private Sprite[] _poopSprites = null;
    [SerializeField]
    private AudioClip _shootClip = null;

    [field: SerializeField]
    private UnityEvent OnPatternEnd = null;

    private Vector3 _originPos = Vector3.zero;
    Sequence _seq = null;

    public void StartUI()
    {
        _soldierUIManager.SetText("Stars", "Countless", "Falling", 1f, 1f, () =>
        {
            StartCoroutine(SpawnPoop());
        });
    }

    private void OnEnable()
    {
        if(_soldierUIManager == null)
        {
            _soldierUIManager = GetComponent<SoldierUIManager>();
            _originPos = transform.position;
        }
        StartUI();
    }

    private IEnumerator SpawnPoop()
    {
        CameraManager.instance.CameraShake(5f, 20f, 0.05f * 200f, true);
        Vector3 pos = new Vector3(0f, 5f);
        for (int i = 0; i < 155; i++)
        {
            Barrage s = PoolManager.Instance.Pop("Barrage") as Barrage;
            s.transform.SetParent(_bossObjectTrm);
            pos.x = Random.Range(-8f, 8f);
            s.transform.SetPositionAndRotation(pos, Quaternion.AngleAxis(180f, Vector3.forward));
            s.SetBarrage(7.5f, new Vector2(0.34f, 0.34f), Vector2.zero, _poopSprites[Random.Range(0, _poopSprites.Length)]);
            s.transform.localScale = Vector3.one * 1f;
            AudioPoolable au = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
            au.Play(_shootClip, 0.6f);
            yield return new WaitForSeconds(0.05f);
        }

        if (_seq != null)
            _seq.Kill();
        _seq = DOTween.Sequence();
        _seq.Append(transform.DOMoveY(8.5f, 0.5f));
        _seq.AppendCallback(() =>
        {
            OnPatternEnd?.Invoke();
        });
    }

    private void OnDisable()
    {
        if (_seq != null)
            _seq.Kill();
        StopAllCoroutines();
        transform.position = _originPos;
    }
}
