using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    
    public int health;
    private Animator animator;
    private GameObject character;
    private NavMeshAgent agent;


    void Start()
    {
        animator = GetComponent<Animator>();
        character = GameObject.Find("Character");
        agent = GetComponent<NavMeshAgent>();
       
    }

    // 게임 오브젝트가 비활성화 되었을때 호출되는 함수입니다.
   

    void Update()
    {
        agent.SetDestination(character.transform.position);
    }
    public void Death()
    {
        if (health <= 0)
        {
            agent.speed = 0;
            
            animator.Play("Die");
            transform.position = ObjectPool.instance.ActivePosition();
             
        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Close"))
        {
            // 현재 애니메이션의 진행도가 1보다 크거나 같다면 User Interface를 비활성화합니다.
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                ObjectPool.instance.InsertQueue(gameObject);
            }
        }
        }
    }
    //오클루전 
    // 카메라가 보는시야만 보이는 기능
    //유니티 정적배칭

    // 1. 원점에서 레이저를 발사합니다
    //hit 에 정보를 저장합니다.
    // 총구 방향에서 레이저가 발사되도록 합니다.
    //hit 저장한 위치에 발사가 되도록 설정합니다.

