using System.Collections;
using UnityEngine;

public class PersonSpawner : MonoBehaviour
{
    [SerializeField] private float timeLimit;
    [SerializeField] private GameObject[] person;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private bool canSpawn;
    [SerializeField] private GameObject spawnWarningPrefab;
    private GameObject spawnWarning;
    [SerializeField, Range(0.2f, 5f)] private float spawnWarningTime = 3f;
    void Update()
    {
        if(canSpawn)
            StartCoroutine(SpawnTimer(timeLimit));
    }
    
    private IEnumerator SpawnTimer(float timer)
    {
        canSpawn = false;
        spawnPoint.x = Random.Range(-8, 8);
        spawnPoint.y--;
        
        spawnWarning = Instantiate(spawnWarningPrefab, spawnPoint, Quaternion.identity);
        yield return new WaitForSeconds(spawnWarningTime);
        Destroy(spawnWarning);
        
        spawnPoint.y++;
        yield return new WaitForSeconds(timer);
        canSpawn = true;
        SpawnDude();
    }

    private void SpawnDude()
    {
        Instantiate(person[Random.Range(0, person.Length)], spawnPoint, Quaternion.identity);
    }
}
