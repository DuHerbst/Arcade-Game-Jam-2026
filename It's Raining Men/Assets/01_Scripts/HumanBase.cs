using UnityEngine;

public class HumanBase : MonoBehaviour
{
    [SerializeField] private float gravityScale;
    [SerializeField] private int pointValue;
    //private bool _hasBounced; -- to be implemented if we have time!
    private Rigidbody2D _rigidbody;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = gravityScale;
        
    }
    
    // void WasBounced()
    // {
    //     _hasBounced = true;
    // }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ScoreZone"))
        {
            //GameManager.Instance.AddScore(pointValue);
            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
            //Game Over, go to end game scene
        }
        
    }
    
}