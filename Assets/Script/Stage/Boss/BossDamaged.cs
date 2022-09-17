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
    [SerializeField]
    private float _effectSize = 1f;
    [SerializeField]
    private int _effectCount = 20;
    [SerializeField]
    private int _lasteffectCount = 10;

    private Color _originColor = Color.white;
    private Color _changeColor = Color.white;
    public Color ChangeColor
    {
        get => _changeColor;
        set => _changeColor = value;
    }

    private bool _isDead = false;
    private bool _isGodMode = false;
    public bool IsGodMode { get => _isGodMode; set => _isGodMode = value; }
    private int _curHp = 100;
    public int HP
    {
        get => _curHp;
        set
        {
            _curHp = value;
            _hpSlider.value = _curHp / (float)_maxHp;
            if (value <= 0 && _isDead == false)
            {
                _isDead = true;
                OnDie?.Invoke();
            }
        }
    }
    [field: SerializeField]
    private UnityEvent OnDie = null;
    [field: SerializeField]
    private UnityEvent OnDieEffectEnd = null;
    [field: SerializeField]
    private UnityEvent<GameObject> DieEnd = null;
    [SerializeField]
    private AudioClip _damageClip = null;
    [SerializeField]
    private AudioClip _explosionClip = null;

    private SpriteRenderer _damageSprite = null;

    private void Awake()
    {
        SetMaxHP();
    }

    protected virtual void Start()
    {
        _damageSprite = GetComponent<SpriteRenderer>();
        _originColor = _damageSprite.color;
        _changeColor = _originColor;
    }

    public void SetMaxHP()
    {
        switch (DifficultyManager.Instance.difficulty)
        {
            case Difficulty.None:
                break;
            case Difficulty.Easy:
                _maxHp = Mathf.RoundToInt(_maxHp / 2f);
                break;
            case Difficulty.Normal:
                break;
            case Difficulty.Hard:
                break;
            case Difficulty.Extreme:
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
        if (collision.CompareTag("PlayerAtk"))
        {
            if (_isGodMode) return;

            HP--;
            AudioPoolable a = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
            a.Play(_damageClip);

            if (HP <= 1)
                return;
            StartCoroutine(DamageCoroutine());
        }
    }

    private IEnumerator DamageCoroutine()
    {
        _damageSprite.color = Color.red;    
        yield return new WaitForSeconds(0.03f);
        _damageSprite.color = _changeColor;
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

        for (int i = 0; i < _effectCount; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.15f));
            Summon();
        }

        for (int i = 0; i < _lasteffectCount; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.05f, 0.08f));
            Summon();
        }
        GetComponent<SpriteRenderer>().enabled = false;

        OnDieEffectEnd?.Invoke();

        yield return new WaitForSeconds(2f);

        DieEnd?.Invoke(Save.Instance.playerMovemant.gameObject);
    }

    private void Summon()
    {
        ShockWave s = PoolManager.Instance.Pop("BossDieEffect") as ShockWave;
        s.transform.SetParent(_bossObjTrm);
        s.transform.position = transform.position + (Vector3)(Random.insideUnitCircle * _randomCircle);
        s.transform.localScale = Vector3.one * _effectSize;
        s.StartAnimation();
        CameraManager.instance.CameraShake(24f, 60f, 0.2f);

        AudioPoolable a = PoolManager.Instance.Pop("AudioPool") as AudioPoolable;
        a.Play(_explosionClip);
    }

    private void OnEnable()
    {
        if (_damageSprite != null)
        {
            _damageSprite.color = _originColor;
            _changeColor = _originColor;
        }
    }

    private void OnDisable()
    {
        _isDead = false;
        _isGodMode = false;
        StopAllCoroutines();
    }
}
