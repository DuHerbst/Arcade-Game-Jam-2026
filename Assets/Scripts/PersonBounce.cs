using UnityEngine;

public class PersonBounce : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 bounceForce;
    [SerializeField] private float direction;
    [SerializeField] private float scoreValue;

    [SerializeField] private GameManager gm;
    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Wall")) return;
        
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("hi");
            if (bounceForce.y < 10)
            {
                scoreValue *= 2;
                SavePerson();
                return;
            }
            
            if (transform.position.x < col.gameObject.transform.position.x)
            {
                direction = -1;
            } 
            else if (transform.position.x == col.transform.position.x)
            {
                direction = 0;
            }
            else
            {
                direction = 1;
            }
            
            bounceForce.x = direction;
            
            rb.AddForce(bounceForce, ForceMode.Impulse);
        }
        bounceForce.y /= 1.5f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (bounceForce.y < 11)
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
        Destroy(gameObject);
    }

    private void LetThemDie()
    {
        if (gm == null)
            gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        
        gm.SomeoneDied();
        Destroy(gameObject);
    }
}
