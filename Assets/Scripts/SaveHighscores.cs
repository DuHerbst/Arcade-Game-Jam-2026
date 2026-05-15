using UnityEngine;

public class SaveHighscores : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    private float currentArrayPosition;
    private float[] highscores;

    void Awake()
    {
        highscores = new float[5];
        LoadScores();
    }

    public void CheckScore(float currentScore)
    {
        for (int j = 0; j < highscores.Length; j++)
        {
            if (highscores[j] > currentScore) continue;

            for (int i = highscores.Length - 1; i > j - 1; i--)
            {
                if (i == 0) continue; 
                
                highscores[i] = highscores[i - 1];
                PlayerPrefs.SetFloat($"Highscore{i}", highscores[i]);

                if (i != j) continue;
                
                highscores[i] = currentScore; 
                break;
            }
            SaveScore(currentScore, j);
            break;
        }
    }
    
    public void SaveScore(float currentScore, float position)
    {
        PlayerPrefs.SetFloat($"Highscore{position}", currentScore);
        PlayerPrefs.Save();
    }

    public void LoadScores()
    {
        for (int i = 0; i < highscores.Length; i++)
        {
            highscores[i] = PlayerPrefs.GetFloat($"Highscore{i}");
        }
        
        gm.LoadScores(highscores);
    }
}
