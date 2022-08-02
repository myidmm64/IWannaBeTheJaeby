using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapSpawner : MonoBehaviour
{
    [SerializeField]
    private float _spawnInterval = 1f;
    [SerializeField]
    private float _leapSpeed = 2f;
    private List<LeapMove> _leaps = new List<LeapMove>();

    private void OnEnable()
    {
        StopCoroutine("SpawnCoroutine");
        StartCoroutine(SpawnCoroutine());
    }

    private void OnDisable()
    {
        foreach (LeapMove a in _leaps)
        {
            a.Push();
        }
        _leaps.Clear();
    }

    private IEnumerator SpawnCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(_spawnInterval);
            LeapMove leap = PoolManager.Instance.Pop("Leap") as LeapMove;
            leap.Speed = _leapSpeed;
            leap.transform.position = transform.position;
            _leaps.Add(leap);
        }
    }

}
