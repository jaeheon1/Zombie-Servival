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
}
