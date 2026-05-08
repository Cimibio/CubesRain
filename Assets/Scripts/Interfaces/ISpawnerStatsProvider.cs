using System;

public interface ISpawnerStatsProvider
{
    event Action ObjectCreated;
    event Action ObjectSpawned;
    event Action ObjectDespawned;
}