using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayIntroMemo : MonoBehaviour
{
    [SerializeField] private GameObject emailPanel;
    [SerializeField] private CanvasGroup fadeBackground;
    [SerializeField] private TMP_Text emailText;

    [SerializeField] private float typingSpeed = 0.03f;
    [SerializeField] private float holdDuration = 1.5f;
    [SerializeField] private float fadeDuration = 1f;

    [SerializeField, TextArea] private string memoText;
    
    //SKIPSCREEN
    private bool _isTyping;
    private bool _memoFullyShown;
    private bool _skipRequested;
    private bool _startRequested;

    private void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame)
        {
            if (_isTyping)
            {
                _skipRequested = true;
            }
            else if (_memoFullyShown)
            {
                _startRequested = true;
            }
        }
    }

    private IEnumerator Start()
    {
        Time.timeScale = 0f;

        emailPanel.SetActive(true);
        fadeBackground.alpha = 0f;
        emailText.text = "";

        yield return StartCoroutine(TypeMemo());

        float timer = 0f;

        while (timer < holdDuration && !_startRequested)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        while (!_startRequested)
        {
            yield return null;
        }

        yield return StartCoroutine(FadeBlack(0f, 1f));

        emailPanel.SetActive(false);

        Time.timeScale = 1f;

        yield return StartCoroutine(FadeBlack(1f, 0f));

        gameObject.SetActive(false);
    }

    private IEnumerator TypeMemo()
    {
        _isTyping = true;
        _memoFullyShown = false;
        _skipRequested = false;

        emailText.text = "";

        foreach (char letter in memoText)
        {
            if (_skipRequested)
            {
                emailText.text = memoText;
                break;
            }

            emailText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        _isTyping = false;
        _memoFullyShown = true;
    }

    private IEnumerator FadeBlack(float startAlpha, float endAlpha)
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            float progress = timer / fadeDuration;

            fadeBackground.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);

            yield return null;
        }

        fadeBackground.alpha = endAlpha;
    }
}