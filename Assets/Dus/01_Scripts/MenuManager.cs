using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour

// to manage the start screen and buttons

{
    
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SeeHighScores()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    
}
