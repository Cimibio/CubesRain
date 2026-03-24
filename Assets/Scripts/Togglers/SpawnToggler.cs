using UnityEngine;

namespace Spawners
{
    public class SpawnToggler<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private Spawner<T> _spawner;
        [SerializeField] private MouseReader _mouseReader;

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
            _spawner.ToggleSpawning();
        }
    }
}
