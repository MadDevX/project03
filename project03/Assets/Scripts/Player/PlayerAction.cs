using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

public class PlayerAction : MonoBehaviour {

    public SpriteRenderer interactionRenderer;
    public Camera mainCamera;
    public float maxDist = 100f;
    public float interactionRange = 3f;
    public int interactionLingerFrames = 20;
    private int interactableMask;
    private CharacterEquipment equip;
	// Use this for initialization
	void Start ()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        interactableMask = LayerMask.GetMask("Interactable");
        equip = GetComponent<CharacterEquipment>();
        interactionRenderer.transform.localScale = new Vector3(interactionRange, interactionRange, 0);
        #region DEBUG COLOR
        Color c = interactionRenderer.color;
        c.a = 0;
        interactionRenderer.color = c;
        #endregion
    }

    // Update is called once per frame
    void Update ()
    {
		if(Input.GetKeyUp(KeyCode.E))
        {
            FindItem();
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            equip.DetachWeapon();
        }
	}

    IEnumerator Fade()
    {
        for (float f = 1f; f >= -2f/interactionLingerFrames; f -= 1f/interactionLingerFrames)
        {
            Color c = interactionRenderer.color;
            if (f < 0)
            {
                c.a = 0;
            }
            else
            {
                c.a = f;
            }
            interactionRenderer.color = c;
            yield return null;
        }
    }

    /// <summary>
    /// Checks if cursor is currently over an interactable objects and interacts if object was found.
    /// </summary>
    void FindItem()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, maxDist, interactableMask);
        if (hit.transform)
        {
            if ((hit.transform.position - transform.position).magnitude <= interactionRange)
            {
                IInteractable interactable = hit.transform.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact(equip);
                }
            }
            else
            {
                StopCoroutine(Fade());
                StartCoroutine(Fade());
            }
        }

    }

    //DEBUG
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
