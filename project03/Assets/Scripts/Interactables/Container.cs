using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterInventory))]
public class Container : InteractableObject
{
    public override bool Interact(GameObject actor)
    {
        //if(inventory.items.Count==0)
        //{
        //    Debug.Log("Container is empty.");
        //    return false;
        //}
        CharacterInventory inv = actor.GetComponent<CharacterInventory>();
        if(inv==null)
        {
            return false;
        }
        //int i;
        //for(i=0;i<inventory.items.Count;i++)
        //{
        //    if(!inv.Add(inventory.items[i]))
        //    {
        //        break;
        //    }
        //}
        //inventory.items.RemoveRange(0, i);
        //Debug.Log(actor.name + " looted " + i + " items from the " + transform.name);
        if (actor.tag == "Player")
        {
            ItemsScrollList.Instance.ShowPanel(this);
        }
        return true;
    }
}
