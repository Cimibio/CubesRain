using Spawners;
using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private SpawnArea _spawnArea;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private float _minCubeLifetime = 2f;
    [SerializeField] private float _maxCubeLifetime = 5f;

    private bool _isSpawning = true;
    private Coroutine _spawnCoroutine;

    public event Action<Vector3> CubeExpiredAtPosition;

    private void Start()
    {
        StartSpawning();
    }

    private void OnDisable()
    {
        StopSpawning();
    }
    protected override void Spawn(Cube cube)
    {
        cube.transform.position = _spawnArea.GetRandomSpawnPoint();

        base.Spawn(cube);
        float lifetime = UnityEngine.Random.Range(_minCubeLifetime, _maxCubeLifetime);
        cube.Init(lifetime);

        cube.Expired += Remove;
    }

    private void Remove(Cube cube)
    {
        Vector3 expiredCubePosition = cube.transform.position;

        cube.Expired -= Remove;
        ReleaseToPool(cube);

        CubeExpiredAtPosition?.Invoke(expiredCubePosition);
    }

    private void StartSpawning()
    {
        if (_spawnCoroutine == null)
        {
            _isSpawning = true;
            _spawnCoroutine = StartCoroutine(SpawnRoutine());
        }
    }

    private void StopSpawning()
    {
        if (_spawnCoroutine != null)
        {
            _isSpawning = false;
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        var wait = new WaitForSeconds(_repeatRate);

        while (_isSpawning)
        {
            GetFromPool();
            yield return wait;
        }
    }
}