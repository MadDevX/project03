using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IPooledObject
{

    public float initialForce = 20f;
    public float explosionRadius = 2f;
    public float explosionForce = 10f;
    public int damage = 25;
    public float ttl = 5f;
    public GameObject explosionEffect;
    private Rigidbody rb;
    private Vector3 lastPosition;
    private ObjectPooler objectPooler;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void OnTriggerStay(Collider other)
    {
        transform.position = lastPosition;
        Explode();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Linecast(lastPosition, transform.position, out hit))
        {
            if (hit.transform != transform)
            {
                transform.position = lastPosition;
                Explode();
            }
        }
        lastPosition = transform.position;
    }

    public void SetMultipliers(float forceMult, float damageMult)
    {
        rb.velocity = transform.forward * initialForce * forceMult;
        damage = (int)(damage * damageMult);
    }

    void Explode()
    {
        //Instantiate(explosionEffect, transform.position, transform.rotation);
        objectPooler.SpawnFromPool("BombExplosion", transform.position, Quaternion.identity);
        Rigidbody colliderRB;
        CharacterHealth ch;
        Vector3 hitVector;
        float calcForce;
        Collider[] collidersHit = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider c in collidersHit)
        {
            hitVector = (c.transform.position - transform.position);
            hitVector.y = 0;
            colliderRB = c.GetComponent<Rigidbody>();
            if(colliderRB!=null && hitVector.magnitude <= explosionRadius)
            {
                calcForce = (explosionRadius - hitVector.magnitude) * explosionForce;
                hitVector = hitVector.normalized * calcForce;
                colliderRB.AddForce(hitVector, ForceMode.Impulse);
                colliderRB.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
            ch = c.GetComponent<CharacterHealth>();
            if (ch!=null)
            {
                float multiplier = (explosionRadius - (c.transform.position - transform.position).magnitude)/explosionRadius;
                ch.TakeDamage(Mathf.Max(1, (int)(damage * multiplier)));
            }
        }
        gameObject.SetActive(false);
    }

    public void OnObjectSpawn()
    {
        lastPosition = transform.position;
        Invoke("DeactivateObject", ttl);
    }

    private void OnDisable()
    {
        CancelInvoke("DeactivateObject");
    }

    void DeactivateObject()
    {
        gameObject.SetActive(false);
    }
}
