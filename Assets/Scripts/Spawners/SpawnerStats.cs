using System;
using UnityEngine;

public class SpawnerStats : MonoBehaviour
{
    [SerializeField] private GameObject _spawnerObject;

    private ISpawnerStatsProvider _spawner;

    public int TotalSpawned { get; private set; }
    public int TotalCreated { get; private set; }
    public int ActiveObjects { get; private set; }

    public event Action StatsChanged;

    private void Awake()
    {
        if (_spawnerObject != null)        
            _spawner = _spawnerObject.GetComponent<ISpawnerStatsProvider>();        

        if (_spawner == null)
            Debug.LogError($"[SpawnerStats] на {gameObject.name} не назначен провайдер статов!");
    }

    private void OnEnable()
    {
        if (_spawner == null) 
            return;

        _spawner.ObjectCreated += CountCreated;
        _spawner.ObjectSpawned += CountSpawned;
        _spawner.ObjectDespawned += CountDespawned;
    }

    private void OnDisable()
    {
        if (_spawner == null) 
            return;

        _spawner.ObjectCreated -= CountCreated;
        _spawner.ObjectSpawned -= CountSpawned;
        _spawner.ObjectDespawned -= CountDespawned;
    }

    private void CountCreated()
    {
        TotalCreated++;
        StatsChanged?.Invoke();
    }

    private void CountSpawned()
    {
        TotalSpawned++;
        ActiveObjects++;
        StatsChanged?.Invoke();
    }

    private void CountDespawned()
    {
        ActiveObjects--;
        StatsChanged?.Invoke();
    }
}