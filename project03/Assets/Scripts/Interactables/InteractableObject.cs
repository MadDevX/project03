using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public InteractableDetails details;
    private Renderer itemRenderer;
    private Color startColor;

    private void Awake()
    {
        itemRenderer = GetComponent<Renderer>();
        startColor = itemRenderer.material.color;
    }

    public virtual void Highlight()
    {
        if (itemRenderer != null)
        {
            itemRenderer.material.color += Color.white / 2;
            DescriptionPanel.Instance.SetPanel(details.ToString());
        }
    }

    public virtual void StopHighlight()
    {
        if (itemRenderer != null)
        {
            itemRenderer.material.color = startColor;
        }
    }

    public virtual bool Interact(GameObject actor)
    {
        Debug.Log(actor.name + " interacted with " + transform.name + ".");
        return true;
    }
}
