using System.Collections;
using UnityEngine;

public class PersonSpawner : MonoBehaviour
{
    [SerializeField] private float timeLimit;
    [SerializeField] private GameObject[] person;
    [SerializeField] private Transform[] spawnPoints; //changed spawning point to multiple
    [SerializeField] private bool canSpawn;
    
    
    [SerializeField] private GameObject spawnWarningPrefab;
    private GameObject _spawnWarning;
    [SerializeField, Range(0.2f, 5f)] private float spawnWarningTime;

    private int _lastSpawnedPoint = -1;
    [SerializeField] private int maxSpawnDistance = 3;
    
    
    void Update()
    {
        if(canSpawn)
            StartCoroutine(SpawnTimer(timeLimit));
    }
    
    private IEnumerator SpawnTimer(float timer)
    { 
        canSpawn = false;
        
        int selectedSpawnIndex = PickSpawnPointIndex();
        Transform selectedSpawnPoint = spawnPoints[selectedSpawnIndex];
        _lastSpawnedPoint = selectedSpawnIndex;
    
        Vector3 warningPosition = selectedSpawnPoint.position + Vector3.down;

        _spawnWarning = Instantiate(spawnWarningPrefab, warningPosition, Quaternion.identity);

        yield return new WaitForSeconds(spawnWarningTime);
        Destroy(_spawnWarning);

        yield return new WaitForSeconds(timer);
        
        SpawnDude(selectedSpawnPoint.position);

        canSpawn = true;
    }

    private int PickSpawnPointIndex() // check last spawned and check which one is within range to spawn
    {
        if (_lastSpawnedPoint == -1)
        {
            return Random.Range(0, spawnPoints.Length);
        }
        
        int minIndex = Mathf.Max(0, _lastSpawnedPoint - maxSpawnDistance);
        int maxIndex = Mathf.Min(spawnPoints.Length - 1, _lastSpawnedPoint + maxSpawnDistance);
        
        return Random.Range(minIndex, maxIndex + 1);
    }

    private void SpawnDude(Vector3 spawnPosition)
    {
        Instantiate(person[Random.Range(0, person.Length)], spawnPosition, Quaternion.identity);
    }
}
