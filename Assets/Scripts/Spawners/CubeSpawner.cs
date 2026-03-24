using Spawners;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _minLifetime = 2f;
    [SerializeField] private float _maxLifetime = 5f;

    protected override void ActionOnGet(Cube cube)
    {
        base.ActionOnGet(cube);

        float lifetime = Random.Range(_minLifetime, _maxLifetime);
        cube.Init(lifetime);

        cube.Expired += OnItemExpired;
    }

    private void OnItemExpired(Cube cube)
    {
        cube.Expired -= OnItemExpired;
        ReleaseToPool(cube);
    }
}
