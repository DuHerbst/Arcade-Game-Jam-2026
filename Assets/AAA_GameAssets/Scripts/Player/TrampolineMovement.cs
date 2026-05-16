using UnityEngine;
using UnityEngine.InputSystem;

public class TrampolineMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    private float _direction;
    
    //AUDIO
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bounceSfx;

    public float Direction
    {
        get { return _direction; }
    }
    
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(speed * _direction * Time.deltaTime, rb.linearVelocity.y, rb.linearVelocity.z);
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
