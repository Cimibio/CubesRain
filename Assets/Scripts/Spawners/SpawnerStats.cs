using System;
using UnityEngine;

public class SpawnerStats : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _spawnerProvider;

    private ISpawnerStatsProvider _spawner;
    private int _totalSpawned;
    private int _totalCreated;
    private int _activeObjects;

    public int TotalSpawned => _totalSpawned;
    public int TotalCreated => _totalCreated;
    public int ActiveObjects => _activeObjects;

    public event Action StatsChanged;

    private void Awake()
    {
        if (_spawnerProvider is ISpawnerStatsProvider provider)
            _spawner = provider;
        else
            _spawner = GetComponent<ISpawnerStatsProvider>();
    }

    private void OnEnable()
    {
        if (_spawner != null)
        {
            _spawner.ObjectCreated += OnObjectCreated;
            _spawner.ObjectSpawned += OnObjectSpawned;
            _spawner.ObjectDespawned += OnObjectDespawned;
        }
    }

    private void OnDisable()
    {
        if (_spawner != null)
        {
            _spawner.ObjectCreated -= OnObjectCreated;
            _spawner.ObjectSpawned -= OnObjectSpawned;
            _spawner.ObjectDespawned -= OnObjectDespawned;
        }
    }

    private void OnObjectCreated() { /* ... */ }
    private void OnObjectSpawned() { /* ... */ }
    private void OnObjectDespawned() { /* ... */ }

    public void Reset() { /* ... */ }
}