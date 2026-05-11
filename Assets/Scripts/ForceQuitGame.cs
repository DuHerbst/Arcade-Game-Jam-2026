using System.Collections;
using UnityEngine;

public class ForceQuitGame : MonoBehaviour
{
    [SerializeField] private TrampolineMovement trampolineMovement;
    private bool clickedButton;
    
    void Update()
    {
        if (trampolineMovement.Direction == 0 && !clickedButton)
        {
            StartCoroutine(QuitGameTimer(180));
        } else
        {
            clickedButton = true;
        }
    }

    private IEnumerator QuitGameTimer(float timer)
    {
        clickedButton = false;
        yield return new WaitForSeconds(timer);
        if (!clickedButton)
            Application.Quit();
        
        clickedButton = false;
    }
}
