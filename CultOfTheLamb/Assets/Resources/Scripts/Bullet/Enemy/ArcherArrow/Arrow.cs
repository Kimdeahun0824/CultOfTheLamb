using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody arrowRigidbody;
    public float speed;

    void Start()
    {
        arrowRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        arrowRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy")
        {
            Destroy(gameObject);
        }
        //gameObject.SetActive(false);
    }
}
