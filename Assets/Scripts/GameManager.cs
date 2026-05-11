using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private float score;

    void Start() 
    {
        IncreaseScore(0);
    }
    
    public void IncreaseScore(float value)
    {
        score += value;
        scoreText.text = $"Score: {score}";
    }

    public void SomeoneDied()
    {
        scoreText.text = "How could you let them die??";
    }
}
