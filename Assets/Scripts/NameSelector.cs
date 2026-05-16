using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class NameSelector : MonoBehaviour
{
    [SerializeField] private string[] characters;
    [SerializeField] private TMP_Text nameText;
    private float currentLetter;
    private string setLetters;
    private bool setName;

    private int letterCounter;

    [SerializeField] private SaveHighscores saveHighscores;
    
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
        if (letterCounter < 3)
        {
            nameText.text = setLetters + characters[(int)currentLetter];
            return;
        }
        
        PlayerPrefs.SetString("newName", setLetters);
        saveHighscores.MovePositions(PlayerPrefs.GetFloat("currentScore"), PlayerPrefs.GetInt("position"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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
