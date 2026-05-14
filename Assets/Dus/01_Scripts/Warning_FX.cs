using System.Collections;
using UnityEngine;

public class SpawnWarningSign : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] warningSprites;
    [SerializeField] private float blinkingTime = 0.2f;

    private Coroutine _warningCoroutine;

    public void ShowWarning(float warningDuration)
    {
        
        gameObject.SetActive(true); // set active so unity doesnt yell at me
        if (_warningCoroutine != null)
        {
            StopCoroutine(_warningCoroutine);
        }

        _warningCoroutine = StartCoroutine(WarningRoutine(warningDuration));
    }

    private IEnumerator WarningRoutine(float warningDuration) // blinking effect and the duration of the warning sign
    {
        float timer = 0f;

        SetSpritesVisible(true);

        while (timer < warningDuration)
        {
            SetSpritesVisible(true);
            yield return new WaitForSeconds(blinkingTime);

            SetSpritesVisible(false);
            yield return new WaitForSeconds(blinkingTime);

            timer += blinkingTime * 2f;
        }

        SetSpritesVisible(false);
        _warningCoroutine = null;
    }

    private void SetSpritesVisible(bool isVisible)
    {
        foreach (SpriteRenderer sprite in warningSprites)
        {
            if (sprite != null)
            {
                sprite.enabled = isVisible;
            }
        }
    }
}