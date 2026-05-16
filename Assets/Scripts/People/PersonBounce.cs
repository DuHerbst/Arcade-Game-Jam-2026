using System.Collections;
using UnityEngine;

public class PersonBounce : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 bounceForce;
    [SerializeField] private float direction;
    [SerializeField] private float scoreValue;
    [SerializeField] private BoxCollider personCollider;

    [Space (20)]
    [Header ("Sprite Shit")]
    [SerializeField] private SpriteRenderer personSprite;
    [SerializeField] private Sprite fallingSprite;
    [SerializeField] private Sprite deadSprite;
    [SerializeField] private Sprite standingSprite;
    [SerializeField] private Sprite savedSprite;
    [SerializeField] private Sprite hitSprite;
    [SerializeField] private PersonWalkAway personWalkAway;
    
    [Space(20)]
    [Header("SFX Shit")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bounceSound;
    [SerializeField] private AudioClip collideSound;
    [SerializeField] private AudioClip deathSound;
    
    [SerializeField] private GameManager gm;
    private Vector3 stopMovement;

    void Start()
    {
        personSprite.sprite = fallingSprite;
    }
    
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

            personSprite.sprite = savedSprite;
            
            audioSource.PlayOneShot(bounceSound);
        }

        if (col.gameObject.CompareTag("Person"))
        {
            personSprite.sprite = hitSprite;

            if (col.gameObject.transform.position.x < transform.position.x)
                personSprite.flipX = false;
            else
                personSprite.flipX = true;
            
            audioSource.PlayOneShot(collideSound);
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
        StopFalling();
        
        if (gm == null)
            gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        
        gm.IncreaseScore(scoreValue);
        personSprite.sprite = standingSprite;
        StartCoroutine(walkTimer(0.5f));
    }

    private void LetThemDie()
    {
        StopFalling();
        
        audioSource.PlayOneShot(deathSound);
        
        if (gm == null)
            gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        
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
