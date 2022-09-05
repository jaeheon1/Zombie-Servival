
using UnityEngine;

public class Soundsystem : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] Clip;
    public static Soundsystem instance;
    private void Awake()
    {

        //내가 찾는 오브젝트의 타입이 Sound
        var soundObject=FindObjectsOfType<Soundsystem>();
        if(soundObject.Length >1)
        {
            //자기 자신을 파괴해서 오직 하나의 SoundSystem 오브젝트만 남길수 있도록 합니다.
            Destroy(gameObject);
        }

    }

   
    void Start()
    {
        if(instance ==null)
        {
            instance = this;
        }
        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }


    public void Sound(int number)
    {
        audioSource.PlayOneShot(Clip[number]);

    }
}
