using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PopupPoolObject : PoolableMono
{
    [SerializeField]
    private TextMeshPro _text = null;
    [SerializeField]
    private Material _normalMat = null;
    [SerializeField]
    private Material _criticalMat = null;
    private MeshRenderer _meshRenderer = null;

    [SerializeField]
    private Color _normalColor = Color.white;
    [SerializeField]
    private Color _criticalColor = Color.white;

    private Sequence _seq = null;

    public void PopupText(Vector3 startPos, Vector3 lastPos, Color color, float duration, int fontSize = 5) // 시작 포지션, 마지막 포지션, 색깔, 폰트사이즈, 사이즈
    {
        startPos.z += 0.5f;
        lastPos.z += 0.5f;
        startPos.y += 2f;
        lastPos.y += 2f;

        transform.position = startPos;
        _text.color = color;
        _text.fontSize = fontSize;

        _seq = DOTween.Sequence();
        _seq.Append(transform.DOMove(lastPos, duration));
        _seq.Join(_text.DOFade(0, duration));
        _seq.AppendCallback(() =>
        {
            PoolManager.Instance.Push(this);
        });
    }

    public void PopupTextNormal(Vector3 startPos, string text) 
    {
        startPos.y += 1f;
        _text.fontSize = 5f;
        _text.color = Color.white;

        Vector3 randomPos = Random.insideUnitSphere * 0.6f;
        randomPos.y = 0f;
        transform.position = startPos += randomPos;
        transform.localScale = Vector3.one * 1.5f;
        _meshRenderer.material = _normalMat;
        _text.SetText(text);

        Sequence seq = DOTween.Sequence();
        seq.Append(_text.DOFade(1f, 0.2f));
        seq.Join(transform.DOScale(0.8f, 0.15f));
        seq.Join(_text.DOColor(_normalColor, 0.05f));
        seq.Append(_text.DOFade(0f, 0.5f));
        seq.AppendCallback(() =>
        {
            PoolManager.Instance.Push(this);
        });
    }
    public void PopupTextCritical(Vector3 startPos, string text)
    {
        startPos.y += 1f;
        _text.fontSize = 5f;
        _text.color = Color.white;

        Vector3 randomPos = Random.insideUnitSphere * 0.6f;
        randomPos.y = 0f;
        transform.position = startPos += randomPos;
        transform.localScale = Vector3.one * 2f;
        _meshRenderer.material = _criticalMat;
        _text.SetText(text);

        CameraManager.instance.CameraShake(2f,2f,0.1f);
        StartCoroutine(FadeCoroutine());

        Sequence seq = DOTween.Sequence();
        seq.Append(_text.DOFade(1f, 0.2f));
        seq.Join(transform.DOScale(1f, 0.15f));
        seq.Join(_text.DOColor(_criticalColor, 0.05f));
        seq.Append(_text.DOFade(0f, 0.5f));

        seq.AppendCallback(() =>
        {
            PoolManager.Instance.Push(this);
        });
    }

    private IEnumerator FadeCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        _text.color = new Color(0f, 0f, 0f, 0f);
    }

    public void PopupJumpWithRandomness(Vector3 startPos, float jumpPower, float randomXmove, Color color, float duration, int fontSize = 5)
    {
        float originPos = startPos.z;
        startPos.z += jumpPower;
        startPos.y += 0.5f;

        transform.position = startPos;
        _text.color = color;
        _text.fontSize = fontSize;

        _seq = DOTween.Sequence();
        _seq.Append(transform.DOMoveZ(originPos, duration).SetEase(Ease.OutBounce));
        _seq.Join(transform.DOMoveX(transform.position.x + Random.Range(-randomXmove, randomXmove), duration));
        _seq.Join(_text.DOFade(0, duration));

        _seq.AppendCallback(() =>
        {
            PoolManager.Instance.Push(this);
        });
    }

    public override void PopReset()
    {
        if (_meshRenderer == null)
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        StopAllCoroutines();

        if (_seq != null)
        {
            _seq.Kill();
        }

        _text.fontSize = 8;

        _text.transform.position = Vector3.zero;
        _text.transform.localScale = Vector3.one;

        _text.color = Color.white;
    }

    public override void PushReset()
    {
    }
}
