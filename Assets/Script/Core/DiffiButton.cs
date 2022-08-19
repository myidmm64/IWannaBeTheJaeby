using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffiButton : MonoBehaviour
{
    [SerializeField]
    private Difficulty _dif = Difficulty.None;

    public void DifSet()
    {
        DifficultyManager.Instance.DifficultySet(_dif);
    }
}
