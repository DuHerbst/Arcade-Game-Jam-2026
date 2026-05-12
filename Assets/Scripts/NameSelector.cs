using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class NameSelector : MonoBehaviour
{
    [SerializeField] private string[] characters;
    [SerializeField] private TMP_Text nameText;
    private float currentLetter;

    void Update()
    {
        nameText.text = characters[(int)currentLetter];
    }

    public void OnSelector(InputValue value)
    {
        currentLetter += value.Get<float>();

        if (currentLetter < 0)
            currentLetter = 27;
        if (currentLetter > 27)
            currentLetter = 0;
    }
}
