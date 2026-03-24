using Spawners;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private float _minCubeLifetime = 2f;
    [SerializeField] private float _maxCubeLifetime = 5f;
    [SerializeField] private int _ySpawnOffset = 10;

    protected override void ActionOnGet(Cube cube)
    {
        base.ActionOnGet(cube);

        cube.transform.position = GetRandomSpawnPoint();
        float lifetime = Random.Range(_minCubeLifetime, _maxCubeLifetime + 1);
        cube.Init(lifetime);

        cube.Expired += OnItemExpired;
    }

    private void OnItemExpired(Cube cube)
    {
        cube.Expired -= OnItemExpired;
        ReleaseToPool(cube);
    }

    private Vector3 GetRandomSpawnPoint()
    {
        if (_startPoint == null) return transform.position;

        Collider col = _startPoint.GetComponent<Collider>();
        Bounds bounds = col.bounds;

        float y = bounds.max.y + _ySpawnOffset;
        float x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float z = UnityEngine.Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x, y, z);
    }
}
