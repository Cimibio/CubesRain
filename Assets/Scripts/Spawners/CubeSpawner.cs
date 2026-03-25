using Spawners;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private SpawnArea _spawnArea;
    [SerializeField] private float _minCubeLifetime = 2f;
    [SerializeField] private float _maxCubeLifetime = 5f;
    
    protected override void Spawn(Cube cube)
    {
        cube.transform.position = _spawnArea.GetRandomSpawnPoint();

        base.Spawn(cube);
        float lifetime = Random.Range(_minCubeLifetime, _maxCubeLifetime);
        cube.Init(lifetime);

        cube.Expired += OnItemExpired;
    }

    private void OnItemExpired(Cube cube)
    {
        cube.Expired -= OnItemExpired;
        ReleaseToPool(cube);
    }
}
