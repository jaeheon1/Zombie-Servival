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

        if (health <= 0)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Close"))
            {
                // 현재 애니메이션의 진행도가 1보다 크거나 같다면 User Interface를 비활성화합니다.
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    transform.position = ObjectPool.instance.ActivePosition();
                    ObjectPool.instance.InsertQueue(gameObject);
                    transform.position = ObjectPool.instance.ActivePosition();

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
            animator.SetBool("Attack", true);
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

