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


    private GameObject _easyTile = null;


    public virtual void Init()
    {
        OnMapReset?.Invoke();
           
        OnMapStarted?.Invoke();

        if(_isDashable)
        {
            Save.Instance.playerMovemant.DashEnable();
        }
        else
        {
            Save.Instance.playerMovemant.DashDisable();
        }


        if(Player._isEasyMode)
        {
            _easyTile = transform.Find("ColliderTile/EasyModeTile").gameObject;
            if (_easyTile != null)
            {
                _easyTile.SetActive(true);
            }
        }

    }

    public virtual void MapExit()
    {
        if(_isDashable)
        {
            Save.Instance.playerMovemant.MovementDieReset();
        }
    }
}
