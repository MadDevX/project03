using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Helper;

public class EnemyShooting : MonoBehaviour {
    
    public float fireDelay = 0.5f;
    public float range = 20f;
    private CharacterEquipment equip;
    private NavMeshAgent nav;
    private float timer;
    private bool hasTarget = false;
    private float attackFrequency = 0.15f;

	// Use this for initialization
	void Awake ()
    {
        equip = GetComponent<CharacterEquipment>();
        nav = GetComponent<NavMeshAgent>();
        timer = 0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
	}

    /// <summary>
    /// Checks for collision and attacks found target using equipped weapon.
    /// </summary>
    IEnumerator Attack()
    {
        while (hasTarget)
        {
            bool noObstacle = true;
            RaycastHit hit;
            if (Physics.Linecast(equip.currentWeapon.bulletSpawn.position, nav.destination, out hit))
            {
                noObstacle = (hit.transform.tag.StartsWith("Character") || hit.transform.tag == "Player" || hit.transform.tag == "Projectile") && (hit.transform.tag != transform.tag);
            }
            Vector3 distance = nav.destination - equip.currentWeapon.transform.position;
            if (equip.currentWeapon != null && noObstacle && distance.magnitude < range && timer >= fireDelay)
            {
                equip.currentWeapon.Shoot();
            }
            yield return new WaitForSeconds(attackFrequency);
        }
    }

    /// <summary>
    /// Sets hasTarget to true and resets character's shooting delay timer
    /// </summary>
    public void TargetFound()
    {
        hasTarget = true;
        StartCoroutine("Attack");
        timer = 0f;
    }

    /// <summary>
    /// Sets hasTarget to false
    /// </summary>
    public void TargetLost()
    {
        hasTarget = false;
        StopAllCoroutines();
    }
}
