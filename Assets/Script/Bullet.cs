using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Instantiate(Resources.Load<GameObject>("WFX_BImpact Dirt"),
            transform.position,
            transform.rotation);
        Destroy(this.gameObject);


    }

}
