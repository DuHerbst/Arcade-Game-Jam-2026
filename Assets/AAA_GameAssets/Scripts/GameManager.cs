using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private TMP_Text namesText;
    [SerializeField] private SaveHighscores saveHighscores;
    public float score;

    private BackgroundMusic _bgMusic;

    void Awake()
    {
        _bgMusic = GetComponent<BackgroundMusic>();
        _bgMusic.PlayMusic();
    }
    
    public void IncreaseScore(float value)
    {
        score += value;
        scoreText.text = $"Score: {score}";
    }

    public void SomeoneDied()
    {
        StartCoroutine(DeathTimer(1));
    }

    private IEnumerator DeathTimer(float timer)
    {
        
        _bgMusic.StopMusic();
        yield return new WaitForSeconds(timer);
        saveHighscores.CheckScore(score);
        
    }

    public void ReturnToStart()
    {
        score = 0;
        SceneManager.LoadScene(1);
    }

    public void LoadScores(float[] highscores, string[] names)
    {
        for (int i = 0; i < highscores.Length; i++)
        {
            highscoreText.text += $"{highscores[i]}\n";
            namesText.text += $"{names[i]}\n";
        }
    }
}
