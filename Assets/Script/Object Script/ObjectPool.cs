using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public RamdomSpawn spawn;
    public static ObjectPool instance;

    //���� ������Ʈ�� objectpool�� �������ִ� ���� ������Ʈ�� �����մϴ�.

    [SerializeField] GameObject zombie;

    // ���� ������Ʈ�� ���� �� �ִ� �ڷᱸ�� Queue �� �����մϴ�.

    public Queue<GameObject> queue = new Queue<GameObject>();


    void Start()
    {
        //static ��ü�� ����ϱ� ���� objectpool�� instance ������ �־��ݴϴ�.
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
