
using UnityEngine;

public class Soundsystem : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] Clip;
    public static Soundsystem instance;
    private void Awake()
    {

        //���� ã�� ������Ʈ�� Ÿ���� Sound
        var soundObject=FindObjectsOfType<Soundsystem>();
        if(soundObject.Length >1)
        {
            //�ڱ� �ڽ��� �ı��ؼ� ���� �ϳ��� SoundSystem ������Ʈ�� ����� �ֵ��� �մϴ�.
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
