using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Map : MonoBehaviour
{
    [field: SerializeField]
    private UnityEvent OnMapReset = null;
    [field: SerializeField]
    private UnityEvent OnMapStarted = null;

    private GameObject _easyTile = null;


    public virtual void Init()
    {
        OnMapReset?.Invoke();
           
        OnMapStarted?.Invoke();

        if(Player._isEasyMode)
        {
            _easyTile = transform.Find("ColliderTile/EasyModeTile").gameObject;
            if (_easyTile != null)
            {
                _easyTile.SetActive(true);
            }
        }

    }
}
