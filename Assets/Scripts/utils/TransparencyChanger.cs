using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class TransparencyChanger : MonoBehaviour
{
    [SerializeField] private float _startAlpha = 1f;
    [SerializeField] private float _endAlpha = 0f;

    private MeshRenderer _meshRenderer;
    private Material _material;
    private Coroutine _transparencyCoroutine;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        if (_meshRenderer != null)
            _material = _meshRenderer.material;
    }

    private void OnDisable()
    {
        StopTransparencyAnimation();
    }

    public void MakeInvisible(float duration)
    {
        StopTransparencyAnimation();

        if (_material == null || duration <= 0)
            return;

        Color color = _material.color;
        color.a = _startAlpha;
        _material.color = color;

        _transparencyCoroutine = StartCoroutine(AnimateTransparency(duration));
    }

    public void StopTransparencyAnimation()
    {
        if (_transparencyCoroutine != null)
        {
            StopCoroutine(_transparencyCoroutine);
            _transparencyCoroutine = null;
        }
    }

    public void SetAlpha(float alpha)
    {
        if (_material != null)
        {
            Color color = _material.color;
            color.a = Mathf.Clamp01(alpha);
            _material.color = color;
        }
    }

    public void ResetAlpha()
    {
        SetAlpha(_startAlpha);
    }

    private IEnumerator AnimateTransparency(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(_startAlpha, _endAlpha, elapsedTime / duration);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(_endAlpha);
        _transparencyCoroutine = null;
    }
}