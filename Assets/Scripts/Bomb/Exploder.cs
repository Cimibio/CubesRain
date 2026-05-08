using UnityEngine;

public class Exploder : MonoBehaviour
{
    private float _explosionRadius = 5f;
    private float _explosionForce = 10f;
    private LayerMask _affectedLayers = -1;

    public void Init(float radius, float force, LayerMask layers = default)
    {
        _explosionRadius = radius;
        _explosionForce = force;

        if (layers != default)
            _affectedLayers = layers;
    }

    public void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius, _affectedLayers);

        foreach (var hit in hitColliders)
        {
            if (hit.gameObject == gameObject)
                continue;

            if (hit.TryGetComponent(out Rigidbody rigidbody))            
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);            
        }
    }
}