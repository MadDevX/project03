using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, ICharacterManager
{
    public List<WeaponDetails> weaponPool;
    private GameController gc;
    private CharacterEquipment equip;
    private CharacterInventory inv;

    private void Awake()
    {
        equip = GetComponent<CharacterEquipment>();
        inv = GetComponent<CharacterInventory>();
        WeaponDetails curWep = weaponPool[Random.Range(0, weaponPool.Count)];
        inv.Add(curWep);
        equip.EquipItem(curWep);
        
    }

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    public void OnDeath()
    {
        foreach (ItemDetails item in inv.items)
        {
            if (Random.Range(.0f, 1.0f) >= .5f)
            {
                GameObject dropped = item.CreatePickup();
                dropped.transform.position = transform.position;
            }
        }
        gc.EnemyKilled();
    }
}
