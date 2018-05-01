using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, ICharacterManager
{
    public List<WeaponDetails> details;
    private GameController gc;
    private CharacterEquipment equip;

    private void Awake()
    {
        gc = FindObjectOfType<GameController>();
        equip = GetComponent<CharacterEquipment>();
        if(equip.currentWeapon==null)
        {
            equip.EquipItem(details[Random.Range(0, details.Count)].CreateWeapon());
        }
    }

    public void OnDeath()
    {
        if (Random.Range(.0f, 1.0f) >= .5f)
        {
            equip.DetachWeapon();
        }
        gc.EnemyKilled();
    }
}
