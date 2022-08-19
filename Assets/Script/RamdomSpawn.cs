using UnityEngine;
using System.Collections;
public class RamdomSpawn : MonoBehaviour
{
    

    void Start()
    {
        StartCoroutine(nameof(CreateZombie));    
    }

    public IEnumerator CreateZombie()
    {
        while(true)
        {


            ObjectPool.instance.GetQueue();
            yield return new WaitForSeconds(10f);

            

        }
    }


    public Vector3 RandomPosition()
    {
        //원의방정식

        /*
        //x^2 + z^2 <=r^2
        //원의 방정식에서 임의의 x 랑 z 해당하는 점이 반지름 r 인 원 안에 존재하는 범위입니다.
        //반지름의 길이 
        // 랜덤으로 가져올 값의 범위는 -반지름 부터 + 반지름의 길이까지 입니다.
        //0.3 ^2+z^2=1
        //z= 루트 1^2 -0.3^2
        //z= 루트 1-0.09
        //z=루트 0.91 
        //z= 0.95
        //반지름 1인 원의 값으로(0.3,0.95)
        */

        //게임 오브젝트를 중심으로 기준 반지름 50인 원을 설정합니다.
        float radius = 100f;

        //첫 번째로 x 값을 계산합니다.
        //원점을 기준으로 -50,50 사이의 나누 값을 생성합니다.

        float x = Random.Range(-radius, radius);

        //방정식
        float z = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x, 2));

        //랜덤으로 0과 1사이의 난수값을 생성하고 0이 나오면 음수 형태의 z 를 z 변수에 넣어주면 됩니다. 
        if(Random.Range(0,2)==0)
        {
            z = -z;
        }

        return new Vector3(x,0,z);
    }
    

    // 캐릭터 백터 -몬스터 벡터 
    // Moveto worlds
    // 네비메쉬 도착지점 함수 (좋은 녀석)

}
