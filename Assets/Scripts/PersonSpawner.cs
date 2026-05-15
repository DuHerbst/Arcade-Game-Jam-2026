using System.Collections;
using UnityEngine;

public class PersonSpawner : MonoBehaviour
{
    [SerializeField] private float timeLimit;
    [SerializeField] private GameObject[] person;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private bool canSpawn;

    void Update()
    {
        if(canSpawn)
            StartCoroutine(SpawnTimer(timeLimit));
    }
    
    private IEnumerator SpawnTimer(float timer)
    {
        canSpawn = false;
        yield return new WaitForSeconds(timer);
        canSpawn = true;
        SpawnDude();
    }

    private void SpawnDude()
    {
        spawnPoint.x = Random.Range(-10, 10);
        Instantiate(person[Random.Range(0, person.Length)], spawnPoint, Quaternion.identity);
    }
}
