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

        private bool _isSpawning = true;
        protected ObjectPool<T> _pool;
        private Coroutine _spawnCoroutine;

        private void Awake()
        {
            _pool = new ObjectPool<T>(
                createFunc: () => Instantiate(_prefab),
                actionOnGet: Activate,
                actionOnRelease: Deactivate,
                actionOnDestroy: (obj) => Destroy(obj.gameObject),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize
            );

            StartSpawning();
        }

        private void OnDisable()
        {
            StopSpawning();
        }

        protected T GetFromPool()
        {
            return _pool.Get();
        }

        protected void ReleaseToPool(T obj)
        {
            _pool.Release(obj);
        }
        protected virtual void Deactivate(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        protected virtual void Activate(T obj)
        {
            obj.gameObject.SetActive(true);
        }

        private void StartSpawning()
        {
            if (_spawnCoroutine == null)
                _spawnCoroutine = StartCoroutine(SpawnRoutine());
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
}
