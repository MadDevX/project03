using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float force = 20;
    public float explosionRadius = 2f;
    public int damage = 25;
    private Rigidbody rb;
    private Vector3 lastPosition;

    // Use this for initialization
    private void Awake()
    {
        lastPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }
    void Start ()
    {
        //rb.velocity = this.transform.forward * force;
    }

    private void OnTriggerEnter(Collider other)
    {
        //var hit = collision.gameObject;
        //if (hit.tag.StartsWith("Character") || hit.tag.StartsWith("Player"))
        //{
        //    CharacterHealth ph;
        //    if ((ph = hit.GetComponent<CharacterHealth>()) != null)
        //    {
        //        ph.TakeDamage(damage);
        //        Destroy(gameObject);
        //    }

        //}
        Explode();
    }

    public void SetMultipliers(float forceMult, float damageMult)
    {
        rb.velocity = this.transform.forward * force * forceMult;
        damage = (int)(damage * damageMult);
    }
    // Update is called once per frame
    void Update ()
    {

	}

    private void FixedUpdate()
    {
        //RaycastHit hit;
        //if (Physics.Raycast(lastPosition, transform.position, out hit, (lastPosition - transform.position).magnitude))
        //{
        //    if (hit.transform != transform)
        //    {
        //        rb.MovePosition(lastPosition);
        //        //Explode();
        //    }
        //}
        //lastPosition = transform.position;
    }

    void Explode()
    {
        Rigidbody colliderRB;
        CharacterHealth ch;
        Vector3 hitVector;
        float calcForce;
        Collider[] collidersHit = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider c in collidersHit)
        {
            hitVector = (c.transform.position - transform.position);
            if (hitVector.magnitude > explosionRadius)
            {
                continue;
            }
            colliderRB = c.GetComponent<Rigidbody>();
            if(colliderRB!=null)
            {
                calcForce = (explosionRadius - hitVector.magnitude) * force;
                hitVector = hitVector.normalized * calcForce;
                colliderRB.AddForce(hitVector, ForceMode.Impulse);
            }
            ch = c.GetComponent<CharacterHealth>();
            if (ch!=null)
            {
                float multiplier = (explosionRadius - (c.transform.position - transform.position).magnitude)/explosionRadius;
                ch.TakeDamage((int)(damage * multiplier));
            }
        }
        Destroy(gameObject);
    }
}
