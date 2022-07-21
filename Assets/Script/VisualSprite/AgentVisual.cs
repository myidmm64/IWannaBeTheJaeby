using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentVisual : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer = null;

    [SerializeField]
    private Color _damagedColor = Color.white;
    [SerializeField]
    private Color _criticalDamageColor = Color.white;
    [SerializeField]
    private int _damageBlinkCnt = 3;

    [SerializeField]
    private Vector2 _localScale = Vector2.zero;

    private Coroutine _damageCoroutine = null;

    private float _lastAmount = 0f;
    private bool _filp = false;

    [field: SerializeField]
    private UnityEvent<bool> OnFaceDirectionChange = null;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void MovementLocalScaleSet(Vector2 localscale)
    {
        _localScale = localscale;
    }

    public void FaceDirectionChange(float amount)
    {

        if (amount < 0 && _filp == false)
        {
            _filp = true;
            _localScale.y *= -1f;
            transform.localScale = -_localScale;
            _localScale.y *= -1f;
            //transform.position += Vector3.right * -0.33f;
            OnFaceDirectionChange?.Invoke(true);
        }
        else if (amount > 0 && _filp == true)
        {
            _filp = false;
            transform.localScale = _localScale;
            //transform.position += Vector3.right * 0.33f;
            OnFaceDirectionChange?.Invoke(false);
        }

        _lastAmount = amount;
    }

    public void DamageColorChange(bool isCritical)
    {
        if(_damageCoroutine != null) StopCoroutine(_damageCoroutine);

        if (isCritical)
        {
            _damageCoroutine = StartCoroutine(DamageVisualCoroutine(_criticalDamageColor));
        }
        else
        {
            _damageCoroutine = StartCoroutine(DamageVisualCoroutine(_damagedColor));
        }
    }

    private IEnumerator DamageVisualCoroutine(Color color)
    {
        for(int i = 0; i<_damageBlinkCnt; i++)
        {
            _spriteRenderer.color = color;
            yield return new WaitForSeconds(0.2f);
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DamageColorChange(false);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            DamageColorChange(true);
        }
        //Color newColor = new Color(Mathf.Cos((Time.time * 20f) * Mathf.Deg2Rad), 0f, 0f);

        _spriteRenderer.color = Color.HSVToRGB(0.5f * Mathf.Cos((Time.time * 20f) * Mathf.Deg2Rad) + 0.5f, 1f,1f);
    }*/
}
