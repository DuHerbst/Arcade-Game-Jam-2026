using System.Collections;
using UnityEngine;

public class WarningFlash : MonoBehaviour
{
    [SerializeField] private SpriteRenderer warningSprite;
    [SerializeField] private Sprite yellow;
    [SerializeField] private Sprite red;
    [SerializeField] private float waitTimer;
    private bool timerBool = true;
    private bool spriteFlipper;
    
    void Update()
    {
        if(timerBool)
            StartCoroutine(SpriteChangeTimer());
    }

    private IEnumerator SpriteChangeTimer()
    {
        timerBool = false;
        warningSprite.sprite = spriteFlipper ?  yellow : red;
        yield return new WaitForSeconds(waitTimer);
        spriteFlipper = !spriteFlipper;
        timerBool = true;
    }
}
