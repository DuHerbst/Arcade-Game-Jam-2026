using UnityEngine;

public class WindowHelpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject helpPopupPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnRate;
    [SerializeField] private float popupLifetime;

    private float _spawnTimer;
    private GameObject _currentHelpPopup;

    private void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= spawnRate)
        {
            SpawnHelpPopup();
            _spawnTimer = 0f;
        }
    }

    private void SpawnHelpPopup()
    {
        if (_currentHelpPopup != null)
        {
            Destroy(_currentHelpPopup);
        }

        Transform selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        _currentHelpPopup = Instantiate(
            helpPopupPrefab,
            selectedSpawnPoint.position,
            Quaternion.identity
        );

        Destroy(_currentHelpPopup, popupLifetime);
    }
}