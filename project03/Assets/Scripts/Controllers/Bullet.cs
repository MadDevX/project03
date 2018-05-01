using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float force = 20;
    public int damage = 25;
    private Rigidbody rb;
    // Use this for initialization
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start ()
    {
        //rb.velocity = this.transform.forward * force;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        if (hit.tag.StartsWith("Character") || hit.tag.StartsWith("Player"))
        {
            CharacterHealth ph;
            if ((ph = hit.GetComponent<CharacterHealth>()) != null)
            {
                ph.TakeDamage(damage);
                Destroy(gameObject);
            }

        }
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
}
