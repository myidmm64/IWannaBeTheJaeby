using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverManager : MonoBehaviour
{
    [SerializeField]
    private List<ObserverableObject> observers = new List<ObserverableObject>();
    private ISubject _target = null;

    private void OnEnable()
    {
        if(_target == null)
        {
            _target = Save.Instance.playerMovemant.GetComponent<ISubject>();
        }
        for (int i = 0; i < observers.Count; i++)
        {
            _target.Sub(observers[i]);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            _target.DisSub(observers[i]);
        }
    }
}
