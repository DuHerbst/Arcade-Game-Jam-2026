using UnityEngine;

public class HumanBase : MonoBehaviour
{
    [SerializeField] private float gravityScale;
    [SerializeField] private int pointValue;
    
    protected Rigidbody2D rigidbodyHuman;
    private HumanSpawner _humanSpawner;
    
    protected virtual void Start()
    {
        rigidbodyHuman = GetComponent<Rigidbody2D>();
        rigidbodyHuman.gravityScale = 0f; //start with no gravity, only fall when we call StartFalling()
        
    }
    public void SetSpawner(HumanSpawner spawner)
    {
        _humanSpawner = spawner;
    }

    public void StartFalling()
    {
      
        rigidbodyHuman.gravityScale = gravityScale;
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ScoreZone"))
        {
            //GameManager.Instance.AddScore(pointValue);
            _humanSpawner.HumanDead();
            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            _humanSpawner.HumanDead();
            Destroy(gameObject);
            //Game Over, go to end game scene
        }

    }
    
}