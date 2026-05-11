using UnityEngine;
using UnityEngine.InputSystem;

public class TrampolineMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    private float direction;

    public float Direction
    {
        get { return direction; }
    }
    
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(speed * direction * Time.deltaTime, rb.linearVelocity.y, rb.linearVelocity.z);
    }
    
    public void OnMove(InputValue value)
    {
        direction = value.Get<float>();
    }
}
