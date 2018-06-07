using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enums;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class ItemDetails : ScriptableObject
{
    public new string name;
    public string description;
    public int price;
    public Sprite icon;

    public Material material;
    public GameObject itemModel;

    public GameObject CreatePickup()
    {
        ItemPickup item = Instantiate(itemModel).GetComponent<ItemPickup>();
        if (item == null)
        {
            Debug.LogWarning("INCOMPATIBLE ITEM MODEL");
            return null;
        }
        item.Stats = this;
        return item.gameObject;
    }

    public virtual void Use(GameObject target)
    {
        //Debug.Log("Using " + name + " on " + target.name);
    }

    public virtual void RemoveFromInventory(GameObject target)
    {
        target.GetComponent<CharacterInventory>().Remove(this);
    }

    public override string ToString()
    {
        return "Name: " + name + "\n\n" + "Description:\n" + description;
    }
}
