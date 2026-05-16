using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    
    [SerializeField] private AudioSource audioSource;
    private bool startedMusic;
    
    void Awake()
    {
        if (startedMusic)
            audioSource.playOnAwake = false;
        else
            startedMusic = true;
    }

    public void PlayMusic()
    {
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
