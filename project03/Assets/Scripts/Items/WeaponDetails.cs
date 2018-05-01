using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class WeaponDetails : ScriptableObject
{
    public new string name;
    public string description;
    public float forceMult = 1f;
    public float damageMult = 1f;
    public float bulletTTL = 5f;
    public float reattackTime = 1f;
    public float effectsDuration = 0.05f;

    public Material material;
    public GameObject weaponModel;
    public GameObject bulletPrefab;

    public Weapon CreateWeapon()
    {
        Weapon wep = Instantiate(weaponModel).GetComponent<Weapon>();
        if(wep==null)
        {
            Debug.Log("INCOMPATIBLE WEAPON MODEL");
            return null;
        }
        wep.Stats = this;
        return wep;
    }
}
