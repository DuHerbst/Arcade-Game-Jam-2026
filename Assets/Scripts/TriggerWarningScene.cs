using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TriggerWarningScene : MonoBehaviour
{
  
    [SerializeField] private float warningDuration;

    private IEnumerator Start()

    {
        yield return new WaitForSeconds(warningDuration);
        SceneManager.LoadScene(2);
    }
    
    
}
