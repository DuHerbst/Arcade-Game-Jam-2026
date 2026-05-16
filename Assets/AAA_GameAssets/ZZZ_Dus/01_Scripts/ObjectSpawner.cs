using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] commonObjects;
    [SerializeField] private GameObject[] midObjects;
    [SerializeField] private GameObject[] rareObjects;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int maxObjects;
    [SerializeField] private float spawnRate;
    private int _currentObjects;
    
    private float _spawnTimer;
    private GameObject _newObject;
    private ObjectsBase _objectBase;

    void Update()
    {
        if (_currentObjects < maxObjects)
        {
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer >= spawnRate)
            {
                SpawnObject();
                _spawnTimer = 0f;
            }
        }
        
    }

    private GameObject PickRandomObject() // choose an object to spawn based on rarity
    {
        int objectSpawned = Random.Range(1, 11);

        if (objectSpawned <= 6)
        {
            return commonObjects[Random.Range(0, commonObjects.Length)];
        }
        
        else if (objectSpawned <= 9)
        {
            return midObjects[Random.Range(0, midObjects.Length)];
        }
        
        else
        {
            return rareObjects[Random.Range(0, rareObjects.Length)];
        }
    }

    void SpawnObject()
    {
        
        if (_currentObjects >= maxObjects)
        {
            return;
        }
        
        _newObject = Instantiate(
            PickRandomObject(), spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity); // spawn objects at a random spawn point
        
        _objectBase = _newObject.GetComponent<ObjectsBase>(); //reference!!

        if (_objectBase != null)
        {
           _objectBase.SetSpawner(this);
        }
        
        _currentObjects++;
        
    }

    public void RemoveObjects()
    {
        
        //when a human gets destroyed, lower the current count so we can spawn more & never let it get below 0
        _currentObjects--;
        _currentObjects = Mathf.Clamp(_currentObjects, 0, maxObjects);
        
        
    }
}