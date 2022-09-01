using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallDalgona : MonoBehaviour
{
    private Rigidbody2D _rigid = null;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Vector3 dir = Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward) * Vector3.up;
        Debug.Log(dir);
        _rigid.AddForce(dir * Random.Range(15f * 0.9f, 15f), ForceMode2D.Impulse);
        //GetComponent<AgentJump>().ForceJump(Random.Range(20f * 0.9f, 20f),dir);
    }
}
