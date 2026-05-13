using UnityEngine;
using System.Collections;
using DG.Tweening;
public class ObjectsBase : MonoBehaviour
{
    [SerializeField] protected float gravityScale;
    [SerializeField] protected int pointValue;
    
    [SerializeField] private Sprite brokenSprite;
    private SpriteRenderer _objectRenderer;
    
    protected Rigidbody2D RigidbodyObject;
    private ObjectSpawner _objectSpawner;
    
    private bool _isBroken;
    
    protected virtual void Start()
    {
        RigidbodyObject = GetComponent<Rigidbody2D>();
        _objectRenderer = GetComponent<SpriteRenderer>();
        
    }
    public void SetSpawner(ObjectSpawner spawner)
    {
        _objectSpawner = spawner;
    }

    private IEnumerator BreakAndDestroy()
    {
        
        yield return new WaitForSeconds(2f);
        _objectRenderer.DOFade(0f, 0.15f).SetLoops(6, LoopType.Yoyo);
        yield return new WaitForSeconds(0.9f);

        if (_objectSpawner != null)
        {
            _objectSpawner.RemoveObjects();
        }
        
        Destroy(gameObject);
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ScoreZone"))
        {
            RigidbodyObject.linearVelocity = Vector2.zero; // vector 2 can't be null? why?
            RigidbodyObject.angularVelocity = 0f;
            RigidbodyObject.gravityScale = 0f;
            
            //GameManager.Instance.AddScore(pointValue);
            
            _objectSpawner.RemoveObjects();
            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Debug.Log("Object hit the ground");
            if (_isBroken) // items can only break once
            {
                return;
            }
            
            _isBroken = true; // if it is not broken yet - now it is
          
            RigidbodyObject.linearVelocity = Vector2.zero; // vector 2 can't be null? why?
            RigidbodyObject.angularVelocity = 0f;
            RigidbodyObject.gravityScale = 0f;

            if (brokenSprite != null)
            {
                _objectRenderer.sprite = brokenSprite;
            }
            
            StartCoroutine(BreakAndDestroy());
            
        }
    }
}

