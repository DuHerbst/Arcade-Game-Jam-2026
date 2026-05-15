using System.Collections;
using UnityEngine;

public class WarningFlash : MonoBehaviour
{
    [SerializeField] private SpriteRenderer warningSprite;
    [SerializeField] private Sprite yellow;
    [SerializeField] private Sprite red;
    [SerializeField] private float waitTimer;
    private bool spriteFlipper;
    
    void Update()
    {
        StartCoroutine(SpriteChangeTimer());
    }

    private IEnumerator SpriteChangeTimer()
    {
        warningSprite.sprite = spriteFlipper ?  yellow : red;
        yield return new WaitForSeconds(waitTimer);
        spriteFlipper = !spriteFlipper;
    }
}
