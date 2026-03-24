using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    private MeshRenderer _renderer;
    private bool _isTouched = false;
    private float _lifetime;

    public event Action<Cube> Expired;
    private Coroutine _expirationCoroutine;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isTouched)
            return;

        if (!collision.gameObject.TryGetComponent<Platform>(out _))
            return;

        _isTouched = true;
        SetRandomColor();

        _expirationCoroutine = StartCoroutine(WaitAndExpire(_lifetime));
    }

    public void Init(float lifetime)
    {
        if (_expirationCoroutine != null)
        {
            StopCoroutine(_expirationCoroutine);
            _expirationCoroutine = null;
        }

        _lifetime = lifetime;
        _isTouched = false;
        _renderer.material.color = Color.white;
    }

    private void NotifyExpired()
    {
        Expired?.Invoke(this);
    }

    private void SetRandomColor()
    {
        _renderer.material.color = UnityEngine.Random.ColorHSV();
    }

    private IEnumerator WaitAndExpire(float delay)
    {
        yield return new WaitForSeconds(delay);
        NotifyExpired();
    }
}