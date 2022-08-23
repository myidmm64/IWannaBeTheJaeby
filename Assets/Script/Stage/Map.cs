using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Map : MonoBehaviour
{
    [SerializeField]
    private bool _isDashable = false;

    [field: SerializeField]
    public UnityEvent OnMapReset = null;
    [field: SerializeField]
    private UnityEvent OnMapStarted = null;


    private GameObject _modeTile = null;


    public virtual void Init()
    {
        OnMapReset?.Invoke();

        OnMapStarted?.Invoke();

        if (_isDashable)
        {
            Save.Instance.playerMovemant.DashEnable();
            Save.Instance.Warring("대시 가능한 지역입니다.");
        }
        else
        {
            Save.Instance.playerMovemant.DashDisable();
        }


        _modeTile = transform.Find($"ColliderTile/{DifficultyManager.Instance.difficulty}ModeTile")?.gameObject;
        if (_modeTile != null)
        {
            _modeTile.SetActive(true);
        }

    }

    public virtual void MapExit()
    {
        if (_isDashable)
        {
            Save.Instance.playerMovemant.MovementDieReset();
        }
    }
}
