using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    private void OnDisable()
    {
        ResetBoss();
    }

    public abstract void ResetBoss();
}
