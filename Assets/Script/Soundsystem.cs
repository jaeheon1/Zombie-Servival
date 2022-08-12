
using UnityEngine;

public class Soundsystem : MonoBehaviour
{
    
    AudioSource audioSource;
    [SerializeField] AudioClip[] clip;

    public static Soundsystem instance;
    void Start()
    {
        if(instance ==null)
        {
            instance = this;
        }
        audioSource = GetComponent<AudioSource>();
    }


    public void Sound(int number)
    {
        audioSource.PlayOneShot(clip[number]);

    }
}
