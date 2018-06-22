using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRaycast : Weapon
{
    public LineRenderer gunLine;
    private Ray shootRay;
    private Vector3 lineEnd;
    private void Start()
    {
        shootRay = new Ray(bulletSpawn.position, bulletSpawn.forward);
        lineEnd = new Vector3(0, 0, Stats.range);
    }

    public override void Shoot()
    {
        if (timer >= reattackTime)
        {
            timer = 0f;
            shootRay.origin = bulletSpawn.position;
            shootRay.direction = bulletSpawn.forward;
            RaycastHit hit;
            if (Physics.Raycast(shootRay, out hit, Stats.range))
            {
                gunLine.SetPosition(1, new Vector3(0, 0, (bulletSpawn.position - hit.transform.position).magnitude));
                CharacterStats stats = hit.transform.GetComponent<CharacterStats>();
                if(stats!=null)
                {
                    stats.TakeDamage((int)Stats.damage);
                }
            }
            else
            {
                gunLine.SetPosition(1, lineEnd);
            }
            EnableEffects();
        }
    }

    protected override void EnableEffects()
    {
        base.EnableEffects();
        gunLine.enabled = true;
    }

    protected override void DisableEffects()
    {
        base.DisableEffects();
        gunLine.enabled = false;
    }
}
