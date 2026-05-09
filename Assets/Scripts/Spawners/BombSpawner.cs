using Spawners;
using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private float _minBombLifetime = 2f;
    [SerializeField] private float _maxBombLifetime = 5f;
    [SerializeField] private float _explosionRadius = 10f;
    [SerializeField] private float _explosionForce = 300f;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void Start()
    {
        if (_cubeSpawner != null)        
            _cubeSpawner.CubeExpiredAtPosition += SpawnAtPosition;        
    }

    private void OnDestroy()
    {
        if (_cubeSpawner != null)        
            _cubeSpawner.CubeExpiredAtPosition -= SpawnAtPosition;        
    }

    private void SpawnAtPosition(Vector3 position)
    {
        Bomb bomb = GetFromPool();

        float lifetime = Random.Range(_minBombLifetime, _maxBombLifetime);
        bomb.Init(position, lifetime, _explosionRadius, _explosionForce);

        bomb.Exploded += Remove;
    }

    private void Remove(Bomb bomb)
    {
        bomb.Exploded -= Remove;
        ReleaseToPool(bomb);
    }
}