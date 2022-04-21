using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grenade : MonoBehaviour
{
    public GameObject explosionFactory;
    public float radius = 5;

    private void OnCollisionEnter(Collision other)
    {

        Collider[] cols = Physics.OverlapSphere(transform.position, radius);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.CompareTag("Enemy"))
            {
                cols[i].gameObject.GetComponent<NavMeshAgent>().enabled = false;
                Rigidbody rb = cols[i].gameObject.AddComponent<Rigidbody>();
                // rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
                // rb.angularVelocity = cols[i].transform.right * 5;
                rb.AddExplosionForce(10, transform.position, radius, 100, ForceMode.Impulse);
                Destroy(cols[i].gameObject, 2);
            }
        }
        GameObject explosion = Instantiate(explosionFactory);
        explosion.transform.position = transform.position;
        Destroy(gameObject);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
