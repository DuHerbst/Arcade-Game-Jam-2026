using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private float spawnTimer;
    [SerializeField] private GameObject[] objects;
    [SerializeField] private Vector3 spawnOffset;
    private bool canSpawn = true;

    void Update()
    {
        if(canSpawn)
            StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnTimer);
        SpawnObject();
        canSpawn = true;
    }

    private void SpawnObject()
    {
        spawnOffset.x = Random.Range(-8, 8);
        
        Instantiate(objects[Random.Range(0, objects.Length)], spawnOffset, Quaternion.identity);
    }
}
