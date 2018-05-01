using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
public class Item : MonoBehaviour, IInteractable
{
    public ItemType itemType;
    protected Renderer itemRenderer;
    private Color startColor;

    private void Awake()
    {
        FindReferences();
        SetProperties();
    }

    /// <summary>
    /// Sets references required for all Item objects.
    /// </summary>
    protected virtual void FindReferences()
    {
        itemRenderer = GetComponent<Renderer>();
    }

    /// <summary>
    /// Updates rendering data.
    /// </summary>
    protected virtual void SetProperties()
    {
        startColor = itemRenderer.material.color;
    }

    private void OnMouseEnter()
    {
        itemRenderer.material.color += Color.white / 2;    }

    private void OnMouseExit()
    {
        itemRenderer.material.color = startColor;
    }

    /// <summary>
    /// Picks up an item.
    /// </summary>
    /// <param name="equip">Reference to character's equipment component.</param>
    /// <returns>True if an item was picked up. False otherwise.</returns>
    public virtual bool Interact(CharacterEquipment equip=null)
    {
        if(equip==null)
        {
            return false;
        }
        equip.EquipItem(this);
        return true;
    }
}
