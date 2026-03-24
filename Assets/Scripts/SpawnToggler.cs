using UnityEngine;

namespace Spawners
{
    public class SpawnToggler<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private Spawner<T> _spawner;
        [SerializeField] private MouseReader _mouseReader;

        private bool _isActive = false;

        private void OnEnable()
        {
            _mouseReader.Clicked += OnMouseClicked;
        }

        private void OnDisable()
        {
            _mouseReader.Clicked -= OnMouseClicked;
        }

        private void OnMouseClicked()
        {
            _isActive = !_isActive;

            if (_isActive)
                _spawner.StartSpawning();
            else
                _spawner.StopSpawning();

            Debug.Log($"Спавнер {(_isActive ? "запущен" : "остановлен")}");
        }
    }
}
