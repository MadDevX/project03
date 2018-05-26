using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammunition", menuName = "Items/Ammunition")]
public class AmmunitionDetails : ItemDetails
{
    public float baseShootingForce = 20f;
    public float baseExplosionRadius = 2f;
    public float baseExplosionForce = 10f;
    public int baseDamage = 25;
    public float ttl = 5f;
    public string hitEffectPrefab;
}
