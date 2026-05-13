using UnityEngine;

public class WomanHuman : HumanBase

{
    [SerializeField] private float floatSpeed;
    [SerializeField] private float leftMax;
    [SerializeField] private float rightMax;

    private int _floatDirection = 1; //1 for right, -1 for left
    
    protected override void Start()
    {
        base.Start();
        StartFalling();
    }

    void Update()
    {
        if (IsDead) // to stop floating!
        {
            return;
        }
        
        if (transform.position.x >= rightMax)
        {
            _floatDirection = -1;
        }
        else if (transform.position.x <= leftMax)
        {
            _floatDirection = 1;
        }
        
        RigidbodyHuman.linearVelocity = new Vector2(_floatDirection * floatSpeed, RigidbodyHuman.linearVelocity.y);
        
    }
    
}
