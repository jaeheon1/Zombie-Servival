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
    //�޸� Ǯ���� �ٽ� Ȱ��ȭ��ų�� ü�°� �ӵ��� �ʱ�ȭ �����ݴϴ�.
    private void OnEnable()
    {
        health = 100;
        //agent.speed = 10;
    }
    // ���� ������Ʈ�� ��Ȱ��ȭ �Ǿ����� ȣ��Ǵ� �Լ��Դϴ�.

    void Update()
    {
        agent.SetDestination(character.transform.position);

        if (health <= 0)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Close"))
            {
                // ���� �ִϸ��̼��� ���൵�� 1���� ũ�ų� ���ٸ� User Interface�� ��Ȱ��ȭ�մϴ�.
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
                // ���� �ִϸ��̼��� ���൵�� 1���� ũ�ų� ���ٸ� User Interface�� ��Ȱ��ȭ�մϴ�.
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    other.GetComponent<Control>().health -= 10;
                    animator.Rebind();// �ʱ�ȭ ���� �׳� �����ϸ� 

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

