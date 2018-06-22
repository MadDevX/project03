using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    public delegate void OnItemChanged(CharacterInventory inv);
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public List<ItemDetails> items = new List<ItemDetails>();

    public bool Add(ItemDetails item)
    {
        if(items.Count >= space)
        {
            //Debug.Log("Inventory full.");
            return false;
        }

        items.Add(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke(this);
        }
        return true;
    }

    public void Remove(ItemDetails item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke(this);
        }
    }

    public List<ItemDetails> GetItemList()
    {
        return items;
    }
}

