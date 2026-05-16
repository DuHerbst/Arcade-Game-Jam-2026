using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TriggerWarningScene : MonoBehaviour
{

    [SerializeField] private float warningDuration = 7f;
    [SerializeField] private AudioClip warningMusic;
    [SerializeField] private AudioSource audioSource;
    
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 1.5f;

    private IEnumerator Start()
    {
        // Start fully black and silent
        fadeCanvasGroup.alpha = 1f;

        audioSource.clip = warningMusic;
        audioSource.volume = 0f;
        audioSource.Play();

        // Fade screen in and music in
        yield return StartCoroutine(FadeIn());

        // Stay on trigger warning screen
        yield return new WaitForSeconds(warningDuration);

        // Fade screen out and music out
        yield return StartCoroutine(FadeOut());
        
        SceneManager.LoadScene(1);
    }

    private IEnumerator FadeIn()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeDuration;

            audioSource.volume = Mathf.Lerp(0f, 1f, progress);
            fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, progress);

            yield return null;
        }

        audioSource.volume = 1f;
        fadeCanvasGroup.alpha = 0f;
    }

    private IEnumerator FadeOut()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeDuration;

            audioSource.volume = Mathf.Lerp(1f, 0f, progress);
            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, progress);

            yield return null;
        }

        audioSource.volume = 0f;
        fadeCanvasGroup.alpha = 1f;
    }
    
}