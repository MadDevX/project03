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
    // Update is called once per frame
    void Update ()
    {
        if (equip.currentWeapon!=null && Input.GetButton("Fire1"))
        {
            equip.currentWeapon.Shoot();
        }
        //if(Input.GetKeyDown(KeyCode.R))
        //{
        //    Time.timeScale = .05f;
        //}
        //if(Input.GetKeyUp(KeyCode.R))
        //{
        //    Time.timeScale = 1f;
        //}
    }


}
