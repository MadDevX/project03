using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class WeaponDetails : EquipmentDetails
{
    public float forceMult = 1f;
    public float damageMult = 1f;
    public float reattackTime = 1f;
    public float effectsDuration = 0.05f;
    public string projectilePrefab;

    /// <summary>
    /// Creates and returns a copy of Equip Model Prefab.
    /// </summary>
    /// <param name="equipment">Out parameter that will be assigned an IEquipment component of the returned object.</param>
    /// <returns></returns>
    public override GameObject CreateEquipped(out IEquipment equipment)
    {
        GameObject equipped = Instantiate(equipModel);
        Weapon wep = equipped.GetComponent<Weapon>();
        equipment = null;
        if(wep==null)
        {
            Debug.LogWarning("INCOMPATIBLE WEAPON MODEL. PLEASE MAKE SURE TO ATTACH WEAPON COMPONENT TO EQUIPPED MODEL VARIANT.");
            return null;
        }
        wep.Stats = this;
        equipment = wep;
        return equipped;
    }

    public override void Use(GameObject target)
    {
        //base.Use(target);
        target.GetComponent<CharacterEquipment>().EquipItem(this);
    }
}
