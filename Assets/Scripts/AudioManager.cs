using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(string path)
    {
        audioSource.Stop();
        var clip = Resources.Load<AudioClip>(path);
        if (clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }    
}
