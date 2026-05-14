using UnityEngine;

public class PersonBounce : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 bounceForce;
    [SerializeField] private float direction;
    [SerializeField] private float scoreValue;

    [SerializeField] private HumanBase humanBase;
    [SerializeField] private GameManager gm;
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall")) return;
        
        if (col.gameObject.CompareTag("Player"))
        {
            if (bounceForce.y < 100)
            {
                scoreValue *= 2;
                SavePerson();
                return;
            }
            
            if (transform.position.x < col.gameObject.transform.position.x)
            {
                direction = -1;
            } else if (transform.position.x == col.transform.position.x)
            {
                direction = 0;
            }
            else
            {
                direction = 1;
            }
            
            bounceForce.x *= direction;
            
            rb.AddForce(bounceForce);
        }
        bounceForce.y /= 1.5f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.gravityScale = 0f;
            
            if (bounceForce.y < 150)
            {
                SavePerson();
                return;
            }
            
            LetThemDie();
        }
    }

    private void SavePerson()
    {
        if (gm == null)
            gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        
        gm.IncreaseScore(scoreValue);
        humanBase.DeadPlayer();
        Destroy(gameObject);
    }

    private void LetThemDie()
    {
        if (gm == null)
            gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        
        gm.SomeoneDied();
        humanBase.DeadPlayer();
        Destroy(gameObject);
    }
}
