using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public RamdomSpawn spawn;
    public static ObjectPool instance;

    //게임 오브젝트를 objectpool에 담을수있는 게임 오브젝트르 설정합니다.

    [SerializeField] GameObject zombie;

    // 게임 오브젝트를 담을 수 있는 자료구조 Queue 를 선언합니다.

    public Queue<GameObject> queue = new Queue<GameObject>();


    void Start()
    {
        //static 객체를 사용하기 위해 objectpool를 instance 변수에 넣어줍니다.
        instance = this;

        for(int i=0;i<20;i++)
        {
            GameObject tempPrefab = Instantiate(zombie, spawn.RandomPosition(),Quaternion.identity);
            queue.Enqueue(tempPrefab);
            tempPrefab.SetActive(false); 
        }


    }

    public Vector3 ActivePosition()
    {
        return spawn.RandomPosition();
    }


 public void InsertQueue(GameObject tobj)
    {
        queue.Enqueue(tobj);

        tobj.SetActive(false);
    }

    public GameObject GetQueue()
    {
        GameObject tempzombie = queue.Dequeue();
        tempzombie.SetActive(true);

        return tempzombie;
    }
   
}
