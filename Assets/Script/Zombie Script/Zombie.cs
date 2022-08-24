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
    //메모리 풀에서 다시 활성화시킬때 체력과 속도를 초기화 시켜줍니다.
    private void OnEnable()
    {
        health = 100;
        //agent.speed = 10;
    }
    // 게임 오브젝트가 비활성화 되었을때 호출되는 함수입니다.

    void Update()
    {
        agent.SetDestination(character.transform.position);

        if (health <= 0)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Close"))
            {
                // 현재 애니메이션의 진행도가 1보다 크거나 같다면 User Interface를 비활성화합니다.
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    transform.position = ObjectPool.instance.ActivePosition();
                    ObjectPool.instance.InsertQueue(gameObject);
                    

                }
            }
        }
    }
    public void Death()
    {
        if (health <= 0)
        {
            agent.speed = 0;

            animator.Play("Die");
        }

    }
    private void  OnTriggerStay(Collider other)
{
        if(other.CompareTag("Character"))
        {
           
            agent.speed = 0;
            transform.LookAt(character.transform);
            animator.SetBool("attack", true);

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            {
                // 현재 애니메이션의 진행도가 1보다 크거나 같다면 User Interface를 비활성화합니다.
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    other.GetComponent<Control>().health -= 10;
                    animator.Rebind();// 초기화 없이 그냥 실행하면 

                }
            }

        }

  }
    private void OnTriggerExit(Collider other)
    {
       if(other.CompareTag("Character"))
        {
            agent.speed = 3.5f;
            animator.SetBool("Attack", false);
        }
    }
}

