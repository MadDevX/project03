using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

public class PlayerShooting : MonoBehaviour
{
    private CharacterEquipment equip;
	// Use this for initialization
	void Awake ()
    {
        equip = GetComponent<CharacterEquipment>();
	}

    public void UseWeapon()
    {
        if (equip.currentWeapon != null)
        {
            equip.currentWeapon.Shoot();
        }
    }
}
