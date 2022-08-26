
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    static public GameManager instance;
    [SerializeField] Text playTime;
    [SerializeField] Text kill;
    public GameObject resultScreen;
    public int count;

    
    void Start()
    {
        Time.timeScale = 1;
        instance = this;
    }

   
    void Update()
    {
        playTime.text = "Running time: 0" + Time.time.ToString("N2");
        kill.text = "Score:0"+count.ToString();
    }
}
