using System;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayers;

    private bool _isTouched = false;

    public event Action Collided;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isTouched)
            return;

        if (IsTargetLayer(collision.gameObject.layer))
        {
            _isTouched = true;
            Collided?.Invoke();
        }

        _isTouched = true;

        Collided?.Invoke();
    }

    public void Init()
    {
        _isTouched = false;
    }

    private bool IsTargetLayer(int layer)
    {
        return (_targetLayers & (1 << layer)) != 0;
    }
}
