using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private MeshRenderer _renderer;

    private bool _isTouched = false;
    private float _lifetime;

    public event Action<Cube> Expired;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void Init(float lifetime)
    {
        _lifetime = lifetime;
        _isTouched = false;
        _renderer.material.color = Color.white;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isTouched)
            return;

        _isTouched = true;
        SetRandomColor();

        Invoke(nameof(NotifyExpired), _lifetime);
    }

    private void NotifyExpired()
    {
        Expired?.Invoke(this);
    }

    private void SetRandomColor()
    {
        _renderer.material.color = UnityEngine.Random.ColorHSV();
    }
}

