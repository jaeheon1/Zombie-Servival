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

    //��Ŭ���� 
    // ī�޶� ���½þ߸� ���̴� ���
    //����Ƽ ������Ī

    // 1. �������� �������� �߻��մϴ�
    //hit �� ������ �����մϴ�.
    // �ѱ� ���⿡�� �������� �߻�ǵ��� �մϴ�.
    //hit ������ ��ġ�� �߻簡 �ǵ��� �����մϴ�.
}