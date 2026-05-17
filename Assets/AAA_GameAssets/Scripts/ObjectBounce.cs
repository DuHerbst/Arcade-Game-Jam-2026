using System.Collections;
using UnityEngine;

public class ObjectBounce : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 bounceForce;
    [SerializeField] private float direction;
    [SerializeField] private float scoreValue;
    [SerializeField] private BoxCollider objectCollider;
    private float _startingBounceForce;
    private Vector3 _stopMovement;

    [SerializeField] private SpriteRenderer objectSprite;
    [SerializeField] private Sprite brokenSprite;
    [SerializeField] private GameManager gm;
    
    //STAR
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private Transform starSpawnPoint;
    
    private bool _showedStar;
    
    //AUDIO
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip crashSfx;
    [SerializeField] private AudioClip dingSfx;
    [SerializeField] private AudioClip objBounceSfx;

    void Start()
    {
        _startingBounceForce = bounceForce.y;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Wall")) return;
        
        if (col.gameObject.CompareTag("Player"))
        {
            if (bounceForce.y < _startingBounceForce / 3)
            {
                objectCollider.isTrigger = true;
                return;
            }
            
            if (col.gameObject.transform.position.x > transform.position.x)
                direction = 1;
            else
                direction = -1;
            
            bounceForce.x = direction;
            
            TrampolineMovement trampoline = col.gameObject.GetComponentInParent<TrampolineMovement>();

            if (trampoline != null)
            {
                trampoline.PlayAudio();
            }

            if (!_showedStar && starPrefab != null && starSpawnPoint != null)
            {
                if (audioSource != null && dingSfx != null)
                {
                    audioSource.PlayOneShot(dingSfx);
                }

                Instantiate(starPrefab, starSpawnPoint.position, Quaternion.identity);
                _showedStar = true;
            }
            
            rb.AddForce(bounceForce, ForceMode.Impulse);
        }
        bounceForce.y /= 1.5f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            if(gm == null)
                gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            
            StopFalling();
            
            if (bounceForce.y < _startingBounceForce / 1.2f)
            {
                SavedObject();
                return;
            }

            LetItFall();
        }
    }

    private void SavedObject()
    {
        gm.IncreaseScore(scoreValue);
        Destroy(gameObject);
    }

    private void LetItFall()
    {

        if (audioSource != null && crashSfx != null)
        {
            audioSource.PlayOneShot(crashSfx);
        }
        
        objectSprite.sprite = brokenSprite;
        StartCoroutine(DestroyedObject(2));
    }

    private void StopFalling()
    {
        //Making it so they cant touch ppl who have hit the ground
        objectCollider.isTrigger = true;
        
        //stopping their movement completely
        rb.useGravity = false;
        rb.linearVelocity = _stopMovement;
        rb.angularVelocity = _stopMovement;

        //Locking their rotation so they're upright
        gameObject.transform.rotation = new Quaternion(0,0,0,0);
    }
    
    private IEnumerator DestroyedObject(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
