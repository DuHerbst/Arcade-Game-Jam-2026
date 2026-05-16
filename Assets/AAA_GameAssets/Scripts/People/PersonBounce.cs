using System.Collections;
using UnityEngine;

public class PersonBounce : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 bounceForce;
    [SerializeField] private float direction;
    
    [SerializeField] private float scoreValue = 1f;
    
    [SerializeField] private BoxCollider personCollider;

    //SPRITES
    [SerializeField] private SpriteRenderer personSprite;
    [SerializeField] private Sprite fallingSprite;
    [SerializeField] private Sprite deadSprite;
    [SerializeField] private Sprite standingSprite;
    [SerializeField] private Sprite savedSprite;
    [SerializeField] private Sprite hitSprite;
    [SerializeField] private PersonWalkAway personWalkAway;

    [SerializeField] private GameObject savedStar;
    [SerializeField] private Transform starSpawnPoint;
    private bool _showedStar;
    
    [SerializeField] private GameManager gm;
    private Vector3 _stopMovement;
    
    //AUDIO
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip yellSfx;
    [SerializeField] private AudioClip splatSfx;
    [SerializeField] private AudioClip dingSfx;
    
    private bool _wasSaved;

    void Start()
    {
        personSprite.sprite = fallingSprite;
        
        if (audioSource != null && yellSfx != null)
        {
            audioSource.PlayOneShot(yellSfx);
        }
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

            TrampolineMovement trampoline = col.gameObject.GetComponentInParent<TrampolineMovement>();

            if (trampoline != null)
            {
                trampoline.PlayAudio();
            }

            if (!_showedStar && savedStar != null && starSpawnPoint != null)
            {
                audioSource.PlayOneShot(dingSfx);
                Instantiate(savedStar, starSpawnPoint.position, Quaternion.identity, transform);
                _showedStar = true;
                
            }

            rb.AddForce(bounceForce, ForceMode.Impulse);

            personSprite.sprite = savedSprite;
        }

        if (col.gameObject.CompareTag("Person"))
        {
            personSprite.sprite = hitSprite;

            if (col.gameObject.transform.position.x < transform.position.x)
                personSprite.flipX = false;
            else
                personSprite.flipX = true;
        }
        bounceForce.y /= 1.5f;
    }

    void OnTriggerEnter(Collider other)
    {

        if (_wasSaved)
        {
            
            return;
            
        }
        
        if (other.gameObject.CompareTag("Ground"))
        {
            _wasSaved = true;
            
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
        StartCoroutine(WalkTimer(0.5f));
    }

    private void LetThemDie()
    {
        StopFalling();
        
        if (audioSource != null && splatSfx != null)
            
        {
            audioSource.PlayOneShot(splatSfx);
        }
        
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
        rb.linearVelocity = _stopMovement;
        rb.angularVelocity = _stopMovement;

        //Locking their rotation so they're upright
        gameObject.transform.rotation = new Quaternion(0,0,0,0);
    }

    private IEnumerator WalkTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        personWalkAway.enabled = true;
    }
}
