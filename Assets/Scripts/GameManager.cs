using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private SaveHighscores saveHighscores;
    public float score;
    
    public void IncreaseScore(float value)
    {
        score += value;
        scoreText.text = $"Score: {score}";
    }

    public void SomeoneDied()
    {
        saveHighscores.CheckScore(score);
        StartCoroutine(DeathTimer(1));
    }

    private IEnumerator DeathTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Test()
    {
        score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void LoadScores(float[] highscores)
    {
        for (int i = 0; i < highscores.Length; i++)
        {
            highscoreText.text += $"{highscores[i]}\n";
        }
    }
}
