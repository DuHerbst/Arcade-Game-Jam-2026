using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class NameSelector : MonoBehaviour
{
    [SerializeField] private string[] characters;
    [SerializeField] private TMP_Text nameText;
    private float currentLetter;
    private string setLetters;
    private bool setName;

    private int letterCounter;

    public string SetLetters
    {
        get => setLetters;
    }

    void Start()
    {
        setName = false;
        setLetters = "";
        letterCounter = 0;
        currentLetter = 0;
    }

    void Update()
    {
        if (letterCounter < 3 && setName)
        {
            nameText.text = setLetters + characters[(int)currentLetter];
        }
    }

    public void OnSelector(InputValue value)
    {
        currentLetter += value.Get<float>();

        if (currentLetter < 0)
            currentLetter = 27;
        if (currentLetter > 27)
            currentLetter = 0;
    }

    public void OnPickLetter()
    {
        setLetters += characters[(int)currentLetter];
        currentLetter = 0;
        letterCounter++;
    }

    public void NewName()
    {
        setName = true;
    }
}
