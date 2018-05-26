using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IPooledObject
{
    [SerializeField] private AmmunitionDetails ammoDetails;

    private Rigidbody rb;
    private Vector3 lastPosition;
    private ObjectPooler objectPooler;

    private float shootingForce;
    private float explosionRadius;
    private float explosionForce;
    private float damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.position = lastPosition;
        Explode();
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

    public void SetMultipliers(float forceMult=1, float damageMult=1, float radiusMult=1, float explosionForceMult=1)
    {
        rb.velocity = transform.forward * ammoDetails.baseShootingForce * forceMult;
        damage = (int)(ammoDetails.baseDamage * damageMult);
        explosionRadius = ammoDetails.baseExplosionRadius * radiusMult;
        explosionForce = ammoDetails.baseExplosionForce * explosionForceMult;
    }

    void Explode()
    {
        objectPooler.SpawnFromPool(ammoDetails.hitEffectPrefab, transform.position, Quaternion.identity);
        Rigidbody colliderRB;
        CharacterStats ch;
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
            }
            ch = c.GetComponent<CharacterStats>();
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
        Invoke("DeactivateObject", ammoDetails.ttl);
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
