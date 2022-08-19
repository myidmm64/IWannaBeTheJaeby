using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossDamaged : MonoBehaviour
{
    [SerializeField]
    private Slider _hpSlider = null;
    [SerializeField]
    private int _maxHp = 100;
    [SerializeField]
    private Transform _bossObjTrm = null;
    [SerializeField]
    private float _randomCircle = 2f;
    private int _curHp = 100;
    public int HP
    {
        get => _curHp;
        set
        {
            _curHp = value;
            _hpSlider.value = _curHp / (float)_maxHp;
            if(value <= 0)
            {
                OnDie?.Invoke();
            }
        }
    }
    [field: SerializeField]
    private UnityEvent OnDie = null;
    [field: SerializeField]
    private UnityEvent<GameObject> DieEnd = null;
    [SerializeField]
    private AudioClip _damageClip = null;
    [SerializeField]
    private AudioClip _explosionClip = null;

    private void Awake()
    {
        switch(Player.difficulty)
        {
            case Player.Difficulty.None:
                break;
            case Player.Difficulty.Easy:
                _maxHp = Mathf.RoundToInt(_maxHp / 2f);
                break;
            case Player.Difficulty.Normal:
                break;
            case Player.Difficulty.Hard:
                _maxHp = Mathf.RoundToInt(_maxHp * 1.5f);
                break;
            case Player.Difficulty.Extreme:
                _maxHp = Mathf.RoundToInt(_maxHp * 2f);
                break;
            default:
                break;
        }
    }

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

    public void SummonDieEffect()
    {
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        Summon();
        yield return new WaitForSeconds(0.5f);
        Summon();
        yield return new WaitForSeconds(0.5f);
        Summon();
        yield return new WaitForSeconds(0.5f);
        Summon();
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i<20; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.15f));
            Summon();
        }
        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(2f);

        DieEnd?.Invoke(Save.Instance.playerMovemant.gameObject);
    }

    private void Summon()
    {
        ShockWave s = PoolManager.Instance.Pop("BossDieEffect") as ShockWave;
        s.transform.SetParent(_bossObjTrm);
        s.transform.position = transform.position + (Vector3)(Random.insideUnitCircle * _randomCircle);
        s.StartAnimation();
        CameraManager.instance.CameraShake(24f, 60f, 0.2f);

        AudioPoolable a = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
        a.Play(_explosionClip);
    }
}
