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
            _spawnerStats.StatsChanged += WriteStats;
    }

    private void OnDisable()
    {
        if (_spawnerStats != null)
            _spawnerStats.StatsChanged -= WriteStats;
    }

    private void Start()
    {
        WriteStats();
    }

    private void WriteStats()
    {
        if (_spawnerStats == null) 
            return;

        if (_totalSpawnedText != null)
            _totalSpawnedText.text = $"{_spawnerStats.TotalSpawned}";

        if (_totalCreatedText != null)
            _totalCreatedText.text = $"{_spawnerStats.TotalCreated}";

        if (_activeObjectsText != null)
            _activeObjectsText.text = $"{_spawnerStats.ActiveObjects}";
    }
}