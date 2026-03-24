using System;
using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

namespace Spawners
{
    public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;
        [SerializeField] private GameObject _startPoint;
        [SerializeField] private float _repeatRate = 1f;
        [SerializeField] private int _poolCapacity = 20;
        [SerializeField] private int _poolMaxSize = 20;
        [SerializeField] private int _ySpawnOffset = 10;

        protected ObjectPool<T> _pool;

        private Coroutine _spawnCoroutine;

        public event Action<T> Spawned;

        private void Awake()
        {
            _pool = new ObjectPool<T>(
                createFunc: () => Instantiate(_prefab),
                actionOnGet: ActionOnGet,
                actionOnRelease: ActionOnRelease,
                actionOnDestroy: (obj) => Destroy(obj.gameObject),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize
            );
        }

        public void StartSpawning()
        {
            if (_spawnCoroutine == null)
                _spawnCoroutine = StartCoroutine(SpawnRoutine());
        }

        public void StopSpawning()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }

        private IEnumerator SpawnRoutine()
        {
            var wait = new WaitForSeconds(_repeatRate);
            while (true)
            {
                GetFromPool();
                yield return wait;
            }
        }

        public T GetFromPool()
        {
            T @object = _pool.Get();
            Spawned?.Invoke(@object);
            return @object;
        }

        public void ReleaseToPool(T obj)
        {
            _pool.Release(obj);
        }

        protected virtual void ActionOnRelease(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        protected virtual void ActionOnGet(T obj)
        {
            obj.transform.position = GetRandomSpawnPoint();
            obj.gameObject.SetActive(true);
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
}
