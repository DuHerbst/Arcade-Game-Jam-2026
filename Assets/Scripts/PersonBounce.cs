using System.Collections;
using UnityEngine;

public class PersonBounce : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 bounceForce;
    [SerializeField] private float direction;
    [SerializeField] private float scoreValue;
    [SerializeField] private BoxCollider personCollider;

    [SerializeField] private SpriteRenderer personSprite;
    [SerializeField] private Sprite fallingSprite;
    [SerializeField] private Sprite deadSprite;
    [SerializeField] private Sprite standingSprite;
    [SerializeField] private PersonWalkAway personWalkAway;
    
    [SerializeField] private GameManager gm;
    private Vector3 stopMovement;
    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Wall")) return;
        
        if (col.gameObject.CompareTag("Player"))
        {
            if (bounceForce.y < 10)
            {
                scoreValue *= 2;
                personCollider.isTrigger = true;
                return;
            }
            
            if (transform.position.x < col.gameObject.transform.position.x)
            {
                direction = -1;
                personSprite.flipX = true;
            } 
            else if (transform.position.x == col.transform.position.x)
            {
                direction = 0;
            }
            else
            {
                direction = 1;
                personSprite.flipX = false;
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
        
        StopFalling();
        gm.IncreaseScore(scoreValue);
        personSprite.sprite = standingSprite;
        StartCoroutine(walkTimer(0.5f));
    }

    private void LetThemDie()
    {
        if (gm == null)
            gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        
        StopFalling();
        gm.SomeoneDied();
        personSprite.sprite = deadSprite;
    }

    private void StopFalling()
    {
        //Making it so they cant touch ppl who have hit the ground
        personCollider.isTrigger = true;
        
        //stopping their movement completely
        rb.useGravity = false;
        rb.linearVelocity = stopMovement;
        rb.angularVelocity = stopMovement;

        //Locking their rotation so they're upright
        gameObject.transform.rotation = new Quaternion(0,0,0,0);
    }

    private IEnumerator walkTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        personWalkAway.enabled = true;
    }
}
