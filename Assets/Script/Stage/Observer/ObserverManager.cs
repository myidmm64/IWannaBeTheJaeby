using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _obserberObj = new List<GameObject>();
    [SerializeField]
    private SubjectState _subjectState = SubjectState.RESTART;

    [SerializeField]
    private List<ObserverableObject> _observers = new List<ObserverableObject>();
    private ISubject _target = null;

    private void OnEnable()
    {
        StartReset();
    }

    private void StartReset()
    {
        if (_target == null)
        {
            _target = Save.Instance.playerMovemant.GetComponent<ISubject>();

            for (int i = 0; i < _obserberObj.Count; i++)
            {
                _observers.Add(new ObserverableObject { observerObj = _obserberObj[i], state = _subjectState });
            }
        }
        for (int i = 0; i < _observers.Count; i++)
        {
            _target.Sub(_observers[i]);
        }

    }

    private void ExitReset()
    {
        for (int i = 0; i < _observers.Count; i++)
        {
            _target.DisSub(_observers[i]);
        }
    }

    private void OnDisable()
    {
        ExitReset();
    }
}
