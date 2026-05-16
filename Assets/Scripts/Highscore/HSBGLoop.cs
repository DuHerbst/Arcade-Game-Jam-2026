using UnityEngine;

public class HSBGLoop : MonoBehaviour
{
    [SerializeField] private RectTransform thisImage;
    [SerializeField] private RectTransform imageInFront;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        if (thisImage.position.x < -1012)
            thisImage.position = imageInFront.position + offset;
        
        transform.Translate(Vector3.left * (Time.deltaTime * speed));
    }
}
