using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Follow : MonoBehaviour
{

    private GameObject character;
    private NavMeshAgent agent;


    void Start()
    {
        character = GameObject.Find("Character");
        agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        agent.SetDestination(character.transform.position);
    }

    //오클루전 
    // 카메라가 보는시야만 보이는 기능
    //유니티 정적배칭

    // 1. 원점에서 레이저를 발사합니다
    //hit 에 정보를 저장합니다.
    // 총구 방향에서 레이저가 발사되도록 합니다.
    //hit 저장한 위치에 발사가 되도록 설정합니다.
}
