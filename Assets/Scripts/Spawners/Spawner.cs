using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Spawners
{
    public abstract class Spawner<T> : MonoBehaviour, ISpawnerStatsProvider where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;
        [SerializeField] private int _poolCapacity = 20;
        [SerializeField] private int _poolMaxSize = 20;

        protected ObjectPool<T> Pool;

        public event Action ObjectCreated;
        public event Action ObjectSpawned;
        public event Action ObjectDespawned;
        public event Action<T> ObjectDestroyed;

        private void Awake()
        {
            Pool = new ObjectPool<T>(
                createFunc: CreateObject,
                actionOnGet: Spawn,
                actionOnRelease: Despawn,
                actionOnDestroy: DestroyObject,
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize
            );
        }

        protected T GetFromPool()
        {
            return Pool.Get();
        }

        private T CreateObject()
        {
            T obj = Instantiate(_prefab);
            ObjectCreated?.Invoke();
            return obj;
        }

        private void DestroyObject(T obj)
        {
            ObjectDestroyed?.Invoke(obj);
            Destroy(obj.gameObject);
        }

        protected void ReleaseToPool(T obj)
        {
            Pool.Release(obj);
        }

        protected virtual void Despawn(T obj)
        {
            ObjectDespawned?.Invoke();
            obj.gameObject.SetActive(false);
        }

        protected virtual void Spawn(T obj)
        {
            ObjectSpawned?.Invoke();
            obj.gameObject.SetActive(true);
        }
    }
}
