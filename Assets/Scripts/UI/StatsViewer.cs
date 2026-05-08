using UnityEngine;
using TMPro;

public class StatsViewer : MonoBehaviour
{
    [SerializeField] private SpawnerStats _spawnerStats;
    [SerializeField] private TextMeshProUGUI _totalSpawnedText;
    [SerializeField] private TextMeshProUGUI _totalCreatedText;
    [SerializeField] private TextMeshProUGUI _activeObjectsText;

    private void OnEnable()
    {
        if (_spawnerStats != null)
            _spawnerStats.StatsChanged += UpdateDisplay;
    }

    private void OnDisable()
    {
        if (_spawnerStats != null)
            _spawnerStats.StatsChanged -= UpdateDisplay;
    }

    private void Start()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (_spawnerStats == null) return;

        if (_totalSpawnedText != null)
            _totalSpawnedText.text = $"Spawned: {_spawnerStats.TotalSpawned}";

        if (_totalCreatedText != null)
            _totalCreatedText.text = $"Created: {_spawnerStats.TotalCreated}";

        if (_activeObjectsText != null)
            _activeObjectsText.text = $"Active: {_spawnerStats.ActiveObjects}";
    }
}