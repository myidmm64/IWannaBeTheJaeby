using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DalgonaGift : MonoBehaviour
{
    [SerializeField]
    private GameObject _bigDalgona = null;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        for(int i = 0; i<4;)
        {
            GameObject big = Instantiate(_bigDalgona, transform.position, Quaternion.identity);
            big.transform.SetParent(transform.parent);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
