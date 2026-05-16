using UnityEngine;

public class PersonWalkAway : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 walkSpeed;
    private float direction = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (gameObject.transform.position.x < 0)
            direction = -1;
        
        walkSpeed.x *= direction;
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = walkSpeed;
    }
}
