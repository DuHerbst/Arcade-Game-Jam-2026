using UnityEngine;
using UnityEngine.InputSystem;

public class ForceQuitGame : MonoBehaviour
{
    [SerializeField] private float quitGameTimer = 180f;
    
    private float _lastInputTime;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _lastInputTime = Time.unscaledTime;
    }

    void Update()
    {
        if (Mouse.current != null && Mouse.current.middleButton.wasPressedThisFrame)
        {
            Debug.Log("Mouse 3 pressed — quitting game");
            Application.Quit();
        }

        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            ResetIdleTimer();
        }

        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame ||
                Mouse.current.rightButton.wasPressedThisFrame ||
                Mouse.current.middleButton.wasPressedThisFrame ||
                Mouse.current.delta.ReadValue().sqrMagnitude > 0.01f)
            {
                ResetIdleTimer();
            }
        }

        if (Time.unscaledTime - _lastInputTime >= quitGameTimer)
        {
            Debug.Log("Idle timer reached — quitting game");
            Application.Quit();
        }
    }

    private void ResetIdleTimer()
    {
        _lastInputTime = Time.unscaledTime;
    }
}