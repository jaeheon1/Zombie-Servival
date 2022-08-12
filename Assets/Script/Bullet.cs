using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{
    [SerializeField] int speed = 5;
    [SerializeField] GameObject origin;
    
    private void Start()
    {
        Destroy(this.gameObject, 3);
    }
    void Update()
    {
        transform.Translate(origin.transform.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            Instantiate(Resources.Load<GameObject>("WFX_BImpact Dirt"),
                transform.position,
                transform.rotation);

             other.transform.GetComponentInParent<AIControl>().health -= 20;
            other.transform.GetComponentInParent<AIControl>().Death();

            Destroy(this.gameObject);
        }

    }

}
