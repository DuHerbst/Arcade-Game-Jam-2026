using UnityEngine;
using System.Collections;

public class SavedStarPopup : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float floatHeight;
    [SerializeField] private float duration;

    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.localPosition;
        StartCoroutine(PopupRoutine());
    }

    private IEnumerator PopupRoutine()
    {
        float timer = 0f;
        Color startColor = spriteRenderer.color;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            transform.localPosition = Vector3.Lerp(
                _startPosition,
                _startPosition + Vector3.up * floatHeight,
                progress
            );

            spriteRenderer.color = new Color(
                startColor.r,
                startColor.g,
                startColor.b,
                Mathf.Lerp(1f, 0f, progress)
            );

            yield return null;
        }

        Destroy(gameObject);
    }
}