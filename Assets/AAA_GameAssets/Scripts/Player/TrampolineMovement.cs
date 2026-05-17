using UnityEngine;
using UnityEngine.InputSystem;

public class TrampolineMovement : MonoBehaviour
{
    //MOVEMENT
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    private float _direction;
    
    [SerializeField] private bool useTrackpadMovement;
    [SerializeField] private float trackpadSensitivity = 0.05f;
    [SerializeField] private float trackpadDeadZone = 0.01f;
    private float _trackpadDirection;
    
    //AUDIO
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bounceSfx;
    
    //ANIMATIONS
    [SerializeField] private Animator animator;
    

    public float Direction
    {
        get { return _direction; }
    }

    private void Update()
    {
        if (!useTrackpadMovement || Mouse.current == null)
        {
            _trackpadDirection = 0f;
            return;
        }

        float mouseX = Mouse.current.delta.ReadValue().x; // not sure if this is working properly...

        if (Mathf.Abs(mouseX) > trackpadDeadZone)
        {
            _trackpadDirection = Mathf.Clamp(mouseX * trackpadSensitivity, -1f, 1f);
        }
        else
        {
            _trackpadDirection = 0f;
        }
    }

    void FixedUpdate()
    {
        float finalDirection = Mathf.Clamp(_direction + _trackpadDirection, -1f, 1f);

        if (animator != null)
        {
            animator.SetBool("Walk_Anim", Mathf.Abs(finalDirection) > 0.01f);
        }
        
        rb.linearVelocity = new Vector3(speed * finalDirection * Time.deltaTime, rb.linearVelocity.y, rb.linearVelocity.z);
    }
    
    public void OnMove(InputValue value)
    {
        _direction = value.Get<float>();
    }
    
    public void PlayAudio()
    {
        
        if (audioSource != null && bounceSfx != null)
        {
            audioSource.PlayOneShot(bounceSfx);
        }
    }
    
}
