
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{

    private NavMeshAgent agent;
    [SerializeField] Transform[] Waypoint;

    public int health = 100;

    private Animator animator;
    private int count;


    private Transform target;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        InvokeRepeating(nameof(MoveNext), 0, 2);
    }

    public void NewTarget(Transform p_target)
    {
        CancelInvoke(nameof(MoveNext));
            target = p_target;

    }

    public void ResetTarget()
    {
        target = null;
        InvokeRepeating(nameof(MoveNext), 0, 2);
    }



    public void MoveNext()
    {
        if (target == null)
        {



            if (agent.velocity == Vector3.zero)
            {
                agent.SetDestination(Waypoint[count++].position);

                if (count >= Waypoint.Length)
                {
                    count = 0;
                }
            }
        }
    }
   
    void Update()
    {
        if(target!=null)
        {
            agent.SetDestination(target.position);
        }

       

    }
    public void Death()
    {
        if (health <= 0)
        {
            agent.speed = 0;
            CancelInvoke();
            animator.Play("Die");
            Destroy(gameObject, 3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Character"))
        {

            NewTarget(other.transform);


        }
    }


    private void OnTriggerExit(Collider other)
    {

        if(other.CompareTag("Character"))
        {
            if(other.CompareTag("Character"))
            {
                ResetTarget();
            }
        }

    }
}
