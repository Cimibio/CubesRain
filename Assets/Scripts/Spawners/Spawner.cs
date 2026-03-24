using System;
using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

namespace Spawners
{
    public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;
        [SerializeField] private float _repeatRate = 1f;
        [SerializeField] private int _poolCapacity = 20;
        [SerializeField] private int _poolMaxSize = 20;
        private bool _isSpawning = false;

        protected ObjectPool<T> _pool;

        private Coroutine _spawnCoroutine;

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

        public void ToggleSpawning()
        {
            _isSpawning = !_isSpawning;

            if (_isSpawning)
                StartSpawning();
            else
                StopSpawning();
        }

        private void StartSpawning()
        {
            if (_spawnCoroutine == null)
                _spawnCoroutine = StartCoroutine(SpawnRoutine());
        }

        protected T GetFromPool()
        {
            return _pool.Get();
        }

        protected void ReleaseToPool(T obj)
        {
            _pool.Release(obj);
        }

        private void StopSpawning()
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

            while (_isSpawning)
            {
                GetFromPool();
                yield return wait;
            }
        }

        protected virtual void ActionOnRelease(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        protected virtual void ActionOnGet(T obj)
        {
            obj.gameObject.SetActive(true);
        }
    }
}
