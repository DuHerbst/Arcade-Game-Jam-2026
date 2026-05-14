using UnityEngine;

public class PersonBounce : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector3 bounceForce;
    [SerializeField] private float direction;
    [SerializeField] private float scoreValue;

    [SerializeField] private HumanBase humanBase;

    [SerializeField] private GameManager gm;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Wall")) return;

        if (col.gameObject.CompareTag("Ground"))
        {
            if (bounceForce.y < 11)
            {
                SavePerson();
                return;
            }
        }
        
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
            
            rb.AddForce(bounceForce);
        }
        bounceForce.y /= 1.5f;
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
