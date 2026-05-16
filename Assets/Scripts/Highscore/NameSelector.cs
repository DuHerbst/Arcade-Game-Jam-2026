using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class NameSelector : MonoBehaviour
{
    [SerializeField] private string[] characters;
    [SerializeField] private TMP_Text nameText;
    private float _currentLetter;
    private string _setLetters;
    private bool _setName;

    private int _letterCounter;

    [SerializeField] private SaveHighscores saveHighscores;
    
    public string SetLetters
    {
        get => _setLetters;
    }

    void Start()
    {
        _setName = false;
        _setLetters = "";
        _letterCounter = 0;
        _currentLetter = 0;
    }

    void Update()
    {
        if (_letterCounter < 3)
        {
            nameText.text = _setLetters + characters[(int)_currentLetter];
            return;
        }
        
        PlayerPrefs.SetString("newName", _setLetters);
        saveHighscores.MovePositions(PlayerPrefs.GetFloat("currentScore"), PlayerPrefs.GetInt("position"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void OnSelector(InputValue value)
    {
        _currentLetter += value.Get<float>();

        if (_currentLetter < 0)
            _currentLetter = 27;
        if (_currentLetter > 27)
            _currentLetter = 0;
    }

    public void OnPickLetter()
    {
        _setLetters += characters[(int)_currentLetter];
        _currentLetter = 0;
        _letterCounter++;
    }

    public void NewName()
    {
        _setName = true;
    }
}
