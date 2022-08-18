using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDamaged : MonoBehaviour
{
    [SerializeField]
    private Slider _hpSlider = null;
    [SerializeField]
    private float _maxHp = 100;
    private float _curHp = 100;
    public float HP
    {
        get => _curHp;
        set
        {
            _curHp = value;
            _hpSlider.value = _curHp / _maxHp;
        }
    }
    [SerializeField]
    private AudioClip _damageClip = null;

    public void ResetHP()
    {
        HP = _maxHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerAtk"))
        {
            HP--;
            AudioPoolable a = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
            a.Play(_damageClip);
        }
    }
}
