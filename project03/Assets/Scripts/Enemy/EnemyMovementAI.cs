using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Helper;
using Enums;

public class EnemyMovementAI : MonoBehaviour {

    public float speed = 6f;
    public float minDistance = 0.2f;
    public float sphereRange = 15f;
    public float slerpFactor = 0.25f;
    private NavMeshAgent nav;
    private EnemyShooting enemyFire;
    private Transform player;
    private CharacterEquipment equip;
    private EnemyAggro agg;
    private Vector3 weaponOffset;

	// Use this for initialization
	void Awake ()
    {
        agg = GetComponentInChildren<EnemyAggro>(true);
        enemyFire = GetComponent<EnemyShooting>();
        nav = GetComponent<NavMeshAgent>();
        equip = GetComponentInChildren<CharacterEquipment>();
        foreach(CharacterEquipment.AttachmentPoint ap in equip.attachmentPoints)
        {
            if(ap.type==EquipmentType.Weapon)
            {
                weaponOffset = ap.position;
            }
        }
	}

    private void Start()
    {
        StartCoroutine(FindTargetAsync(sphereRange, "Player"));
    }
    
    void Update ()
    {
        if(player != null)
        {
            TrackTarget();
        }
	}
    /// <summary>
    /// Checks for objects with the specific tag in the overlap sphere of given radius
    /// </summary>
    void FindTarget(float radius, string tag)
    {
        bool targetFound = false;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider col in hitColliders)
        {
            if (col.tag == tag)
            {
                player = col.transform;
                enemyFire.TargetFound();
                agg.gameObject.SetActive(true);
                targetFound = true;
                break;
            }
        }
        if(!targetFound)
        {
            TargetLost();
        }
    }


    IEnumerator FindTargetAsync(float radius, string tag)
    {
        while (true)
        {
            if (player == null)
            {
                FindTarget(radius, tag);
            }
            yield return new WaitForSeconds(.1f);
        }
    }

    /// <summary>
    /// Checks if the target is not too far to be chased and rotates character to face the target's direction.
    /// </summary>
    void TrackTarget()
    {
        if ((player.position - transform.position).magnitude > 3 * sphereRange)
        {
            TargetLost();
        }
        else
        {
            Vector3 startVector;
            nav.SetDestination(player.position);
            startVector = transform.position + transform.rotation * weaponOffset;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - startVector), slerpFactor);
        }
    }

    /// <summary>
    /// Removes character aggro on previous target
    /// </summary>
    void TargetLost()
    {
        player = null;
        nav.SetDestination(transform.position);
        enemyFire.TargetLost();
        agg.gameObject.SetActive(false);
    }
}
