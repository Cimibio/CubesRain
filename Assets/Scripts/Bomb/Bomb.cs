using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LifeTimer), typeof(Exploder), typeof(ColorChanger))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private float _startAlpha = 1f;
    [SerializeField] private float _endAlpha = 0f;

    private LifeTimer _lifeTimer;
    private Exploder _exploder;
    private Material _material;
    private ColorChanger _colorChanger;
    private float _maxLifeTime;
    private Coroutine _transparencyCoroutine;

    public event Action<Bomb> Exploded;

    private void Awake()
    {
        _lifeTimer = GetComponent<LifeTimer>();
        _exploder = GetComponent<Exploder>();
        _colorChanger = GetComponent<ColorChanger>();

        if (_meshRenderer != null)
            _material = _meshRenderer.material;
    }

    private void OnEnable()
    {
        _lifeTimer.Expired += Explode;
    }

    private void OnDisable()
    {
        _lifeTimer.Expired -= Explode;

        if (_transparencyCoroutine != null)
        {
            StopCoroutine(_transparencyCoroutine);
            _transparencyCoroutine = null;
        }
    }

    public void Init(Vector3 position, float lifeTime, float explosionRadius, float explosionForce)
    {
        transform.position = position;
        _maxLifeTime = lifeTime;
        _colorChanger.SetBlackColor();

        _exploder.Init(explosionRadius, explosionForce);
        _lifeTimer.StartTimer(lifeTime);

        if (_material != null)
        {
            Color color = _material.color;
            color.a = _startAlpha;
            _material.color = color;
        }

        if (_transparencyCoroutine != null)
            StopCoroutine(_transparencyCoroutine);

        _transparencyCoroutine = StartCoroutine(UpdateTransparency());
    }

    private IEnumerator UpdateTransparency()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _maxLifeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(_startAlpha, _endAlpha, elapsedTime / _maxLifeTime);

            if (_material != null)
            {
                Color color = _material.color;
                color.a = alpha;
                _material.color = color;
            }

            yield return null;
        }
    }

    private void Explode()
    {
        _exploder.Explode();
        Exploded?.Invoke(this);
    }
}