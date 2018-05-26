using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : CharacterInventory
{
    public static PlayerInventory Instance;

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else
        {
            CopyFrom(Instance);
            Instance = this;
        }
    }

    private void CopyFrom(PlayerInventory pl)
    {
        onItemChangedCallback = pl.onItemChangedCallback;
        space = pl.space;
        items = pl.items;
    }
}
