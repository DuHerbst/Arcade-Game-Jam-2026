using UnityEngine;

public class SaveHighscores : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private NameSelector nameSelector;
    private float currentArrayPosition;
    private float[] highscores;
    private string[] names;

    void Awake()
    {
        highscores = new float[5];
        names = new string[5];
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
                names[i] = names[i - 1];
                PlayerPrefs.SetString($"Name{i}", names[i]);

                if (i != j) continue;
                
                highscores[i] = currentScore; 
                nameSelector.NewName();
                break;
            }
            SaveScore(currentScore, j, nameSelector.SetLetters);
            break;
        }
    }
    
    public void SaveScore(float currentScore, float position, string name)
    {
        PlayerPrefs.SetFloat($"Highscore{position}", currentScore);
        PlayerPrefs.SetString($"Name{position}", name);
        PlayerPrefs.Save();
    }

    public void LoadScores()
    {
        for (int i = 0; i < highscores.Length; i++)
        {
            highscores[i] = PlayerPrefs.GetFloat($"Highscore{i}");
            //names[i] = PlayerPrefs.GetString($"Name{i}");
        }
        
        gm.LoadScores(highscores, names);
    }
}
