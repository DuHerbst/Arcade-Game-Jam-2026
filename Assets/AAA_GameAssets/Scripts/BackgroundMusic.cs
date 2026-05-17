using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    
    [SerializeField] private AudioSource audioSource;
    private bool _startedMusic;
    
    void Awake()
    {
        if (_startedMusic)
            audioSource.playOnAwake = false;
        else
            _startedMusic = true;
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
