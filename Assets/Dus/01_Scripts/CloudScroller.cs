using UnityEngine;

public class CloudScroller : MonoBehaviour
{
    [SerializeField] private float cloudSpeed;
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    
    private int _direction = 1;

    private void Update()
    {
        transform.position += Vector3.right * _direction * cloudSpeed * Time.deltaTime; 

        if (transform.position.x >= rightLimit) // instad of  instantiationg another image, these will just bounce left and right 
        {
            _direction = -1;
        }
        else if (transform.position.x <= leftLimit)
        {
            _direction = 1;
        }
    }
}
