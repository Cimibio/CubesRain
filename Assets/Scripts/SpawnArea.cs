using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SpawnArea : MonoBehaviour
{
    [SerializeField] private int _ySpawnOffset = 10;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();

        if (_collider == null)
        {
            Debug.LogError($"Collider not found on {gameObject.name}! Please add a Collider component.");
            return;
        }

        _collider.isTrigger = true;
    }

    public Vector3 GetRandomSpawnPoint()
    {
        if (_collider == null)
        {
            Debug.LogError("Cannot get spawn point: Collider is null!");
            return Vector3.zero;
        }

        Bounds bounds = _collider.bounds;

        float y = bounds.max.y + _ySpawnOffset;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x, y, z);
    }
}
