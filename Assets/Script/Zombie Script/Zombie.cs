using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] float maxHealth;
    public float health;
    private Animator animator;
    private GameObject character;
    private NavMeshAgent agent;


    void Start()
    {
        maxHealth = health;
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
        float tempHealth = 1 - (health / maxHealth);

        DistanceSensor();
        animator.SetLayerWeight(animator.GetLayerIndex("Other Layer"),tempHealth);


        agent.SetDestination(character.transform.position);

        if (health <= 0)
        {
            agent.speed = 0;

            animator.Play("Die");
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            {
                // ���� �ִϸ��̼��� ���൵�� 1���� ũ�ų� ���ٸ� User Interface�� ��Ȱ��ȭ�մϴ�.
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    transform.position = ObjectPool.instance.ActivePosition();
                    ObjectPool.instance.InsertQueue(gameObject);
                    

                }
            }
        }
        else
        {
            DistanceSensor();
            agent.SetDestination(character.transform.position);
        }
    }
    
    private void  OnTriggerStay(Collider other)
{
        if(other.CompareTag("Character"))
        {
           
            //agent.speed = 0;
           // transform.LookAt(character.transform);
            //animator.SetBool("attack", true);

            //if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            //{
            //    // ���� �ִϸ��̼��� ���൵�� 1���� ũ�ų� ���ٸ� User Interface�� ��Ȱ��ȭ�մϴ�.
            //    if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            //    {
            //        other.GetComponent<Control>().health -= 10;
            //        animator.Rebind();// �ʱ�ȭ ���� �׳� �����ϸ� 

            //    }
            //}

        }

  }

    public void DistanceSensor()
    {

        //ĳ������ ��ġ�� ���� 5 ���� �۴ٸ� ���� �Ÿ��� �ٰ� ���� ���߰� �ϴ� ���
        if(Vector3.Distance(character.transform.position,transform.position)<=2.5f)
        {
            agent.speed = 0;
            transform.LookAt(character.transform);
            animator.SetBool("attack", true);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            {
                // ���� �ִϸ��̼��� ���൵�� 1���� ũ�ų� ���ٸ� User Interface�� ��Ȱ��ȭ�մϴ�.
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    character.GetComponent<Control>().health -= 10;
                    animator.Rebind();// �ʱ�ȭ ���� �׳� �����ϸ� 

                }
            }
        }
        else // ĳ�����̤� ��ġ�� ���� �ڽ��� �Ÿ��� 5 ���� �־����ٸ�
        {
            agent.speed = 3.5f;
            animator.SetBool("attack", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
       if(other.CompareTag("Character"))
        {
            //agent.speed = 3.5f;
           // animator.SetBool("attack", false);
        }
    }
}

