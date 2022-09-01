using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDalgona : MonoBehaviour
{
    [SerializeField]
    private GameObject _smallDalgona = null;

    public void SpawnSmallDalgona()
    {
        GameObject dal = Instantiate(_smallDalgona, transform.parent);
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(15f, 30f)));
        dal.transform.SetPositionAndRotation(transform.position, rot);

        GameObject dal2 = Instantiate(_smallDalgona, transform.parent);
        Quaternion rot2 = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(-15f, -30f)));
        dal2.transform.SetPositionAndRotation(transform.position, rot2);

        Destroy(dal, 2f);
        Destroy(dal2, 2f);
    }
}
