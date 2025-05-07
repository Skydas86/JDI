using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource effectAudioSource;
    [SerializeField] private AudioSource defaultAudioSource;
    [SerializeField] private AudioClip slashClip;

    public void PlaySlashSound()
    {
        effectAudioSource.PlayOneShot(slashClip); 
    }
    public void PlayDefaultAudio()
    {
        defaultAudioSource.Play();
    }
}
