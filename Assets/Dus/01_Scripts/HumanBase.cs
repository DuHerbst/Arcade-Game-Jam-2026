using UnityEngine;
using System.Collections;

using DG.Tweening;

public class HumanBase : MonoBehaviour
{
    [SerializeField] private float gravityScale;
    [SerializeField] private int pointValue;
    
    [SerializeField] private Sprite deadHuman;
    private SpriteRenderer _humanRenderer;
    
    protected Rigidbody2D RigidbodyHuman;
    private HumanSpawner _humanSpawner;
    
    protected bool IsDead;
    
    protected virtual void Start()
    {
        RigidbodyHuman = GetComponent<Rigidbody2D>();
        _humanRenderer = GetComponent<SpriteRenderer>();
        RigidbodyHuman.gravityScale = 0f; //start with no gravity, only fall when we call StartFalling()
        
    }
    public void SetSpawner(HumanSpawner spawner)
    {
        _humanSpawner = spawner;
    }

    public void StartFalling()
    {
      
        RigidbodyHuman.gravityScale = gravityScale;
        
    }

    private IEnumerator DeathCycle()
    {
        yield return new WaitForSeconds(1.5f);
        _humanRenderer.DOFade(0f, 0.15f).SetLoops(6, LoopType.Yoyo);
        yield return new WaitForSeconds(0.8f);
        
        if (_humanSpawner != null)
        {
            _humanSpawner.HumanDead();
        }
        
        Destroy(gameObject);
        
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
            RigidbodyHuman.linearVelocity = Vector2.zero;
            RigidbodyHuman.angularVelocity = 0f;
            RigidbodyHuman.gravityScale = 0f;
            
            if (deadHuman != null)
            {
                _humanRenderer.sprite = deadHuman;
            }
            
            IsDead = true;
            
            StartCoroutine(DeathCycle());
            _humanSpawner.HumanDead();
            
            
            //Game Over, go to end game scene
        }

        if (other.CompareTag("Player"))
        {
            Debug.Log("Human has touched the trampoline");
            RigidbodyHuman.linearVelocity = new Vector2(RigidbodyHuman.linearVelocity.x, RigidbodyHuman.linearVelocity.y * -1);
        }

    }
    
}