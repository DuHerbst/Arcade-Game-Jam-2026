using UnityEngine;
using System.Collections;

public class HumanSpawner : MonoBehaviour

{
    [SerializeField] private GameObject[] humanPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int maxHumansAlive;
    [SerializeField] private float spawnRate;
    private int _currentHumansAlive;
    
    [SerializeField] private SpawnWarningSign warningSign; //reference to the warning sign script
    [SerializeField] private float warningDuration;
    
    private float _spawnTimer;
    private GameObject _newHuman;
    private HumanBase _humanBase;

    void Update()
    {
        if (1 == 1)
        {
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer >= spawnRate)
            {
                StartCoroutine(SpawnHuman());
                _spawnTimer = 0f;
            }
        }
        
    }

    IEnumerator SpawnHuman()
    {
        
        if (_currentHumansAlive >= maxHumansAlive)
        {
            yield break;
        }
        
        Transform selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        if (warningSign != null)
        {
            warningSign.transform.position = selectedSpawnPoint.position;
            warningSign.ShowWarning(warningDuration);
        }
        
        yield return new WaitForSeconds(warningDuration);
        
        _newHuman = Instantiate
        (humanPrefabs[Random.Range(0, humanPrefabs.Length)],
            selectedSpawnPoint.position, Quaternion.identity); // random humans from pool into random spawn points
        
        _humanBase = _newHuman.GetComponent<HumanBase>(); //reference!!

        if (_humanBase != null)
        {
            _humanBase.SetSpawner(this);
        }
        
        _currentHumansAlive++;
        
    }

    public void HumanDead()
    {
        //when a human gets destroyed, lower the current count so we can spawn more & never let it get below 0
        _currentHumansAlive--;
        _currentHumansAlive = Mathf.Clamp(_currentHumansAlive, 0, maxHumansAlive);
        
    }
    
}
