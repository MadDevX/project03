using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[RequireComponent(typeof(Rigidbody))]
public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] protected ItemDetails stats;
    private Renderer itemRenderer;
    private Color startColor;

    public ItemDetails Stats
    {
        get
        {
            return stats;
        }
        set
        {
            stats = value;
            SetProperties();
        }
    }

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
        if (stats != null)
        {
            startColor = stats.material.color;
            itemRenderer.material = stats.material;
        }
    }

    public virtual void Highlight()
    {
        if (itemRenderer != null)
        {
            itemRenderer.material.color += Color.white / 2;
            DescriptionPanel.Instance.SetPanel(stats.ToString());
        }
    }

    public virtual void StopHighlight()
    {
        if (itemRenderer != null)
        {
            itemRenderer.material.color = startColor;
        }
    }

    /// <summary>
    /// Picks up an item if possible. Returns whether the operation was successful.
    /// </summary>
    /// <param name="actor">Reference to the GameObject that called this method.</param>
    /// <returns>True if an item was picked up. False otherwise.</returns>
    public bool Interact(GameObject actor = null)
    {
        CharacterInventory inv = actor.GetComponent<CharacterInventory>();
        if (inv == null)
        {
            return false;
        }
        return PickUp(inv);
    }

    bool PickUp(CharacterInventory inv)
    {
        if (inv.Add(stats))
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
