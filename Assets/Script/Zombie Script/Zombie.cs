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

    // ���� ������Ʈ�� ��Ȱ��ȭ �Ǿ����� ȣ��Ǵ� �Լ��Դϴ�.
   

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
            // ���� �ִϸ��̼��� ���൵�� 1���� ũ�ų� ���ٸ� User Interface�� ��Ȱ��ȭ�մϴ�.
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                ObjectPool.instance.InsertQueue(gameObject);
            }
        }
        }
    }
    //��Ŭ���� 
    // ī�޶� ���½þ߸� ���̴� ���
    //����Ƽ ������Ī

    // 1. �������� �������� �߻��մϴ�
    //hit �� ������ �����մϴ�.
    // �ѱ� ���⿡�� �������� �߻�ǵ��� �մϴ�.
    //hit ������ ��ġ�� �߻簡 �ǵ��� �����մϴ�.

