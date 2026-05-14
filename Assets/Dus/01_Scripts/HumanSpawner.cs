using UnityEngine;

public class HumanSpawner : MonoBehaviour

{
    [SerializeField] private GameObject[] humanPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int maxHumansAlive;
    [SerializeField] private float spawnRate;
    private int _currentHumansAlive;
    
    private float _spawnTimer;
    private GameObject _newHuman;
    private HumanBase _humanBase;

    void Update()
    {
        if (_currentHumansAlive < maxHumansAlive)
        {
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer >= spawnRate)
            {
                SpawnHuman();
                _spawnTimer = 0f;
            }
        }
        
    }

    void SpawnHuman()
    {
        
        if (_currentHumansAlive >= maxHumansAlive)
        {
            return;
        }
        
        _newHuman = Instantiate
        (humanPrefabs[Random.Range(0, humanPrefabs.Length)],
            spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        
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