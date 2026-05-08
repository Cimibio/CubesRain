using System;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private bool _isTouched = false;

    public event Action Collided;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isTouched)
            return;

        if (!collision.gameObject.TryGetComponent<Platform>(out _))
            return;

        _isTouched = true;

        Collided?.Invoke();
    }

    public void Init()
    {
        _isTouched = false;
    }
}
