using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;


    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }
}
