using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using UnityEngine.EventSystems;

public class PlayerAction : MonoBehaviour {

    public SpriteRenderer interactionRenderer;
    public float maxDist = 100f;
    public float interactionRange = 3f;
    public int interactionLingerFrames = 20;
    private int interactableMask;
    private IInteractable activeObject;
    //private Camera mainCamera;

    private IInteractable ActiveObject
    {
        get
        {
            return activeObject;
        }

        set
        {
            if (activeObject != null)
            {
                activeObject.StopHighlight();
            }
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                DescriptionPanel.Instance.HidePanel();
            }
            activeObject = value;
            if (activeObject != null)
            {
                activeObject.Highlight();
            }
        }
    }

    void Start()
    {
        //if (mainCamera == null)
        //{
        //    mainCamera = Camera.main;
        //}
        LevelLoader.Instance.loadFinished += LevelEnter;
        interactableMask = LayerMask.GetMask("Interactable");
        interactionRenderer.transform.localScale = new Vector3(interactionRange, interactionRange, 0);
        ActiveObject = null;
        StartCoroutine(HighlightInteractable());
        #region DEBUG COLOR
        Color c = interactionRenderer.color;
        c.a = 0;
        interactionRenderer.color = c;
        #endregion
    }

    IEnumerator Fade()
    {
        for (float f = 1f; f >= -2f/interactionLingerFrames; f -= 1f/interactionLingerFrames)
        {
            Color c = interactionRenderer.color;
            if (f < 0)
            {
                c.a = 0;
                interactionRenderer.color = c;
                yield break;
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
    public void InteractWith()
    {
        if(ActiveObject!=null)
        {
            if(ActiveObject.Interact(gameObject))
            {
                ActiveObject = null;
            }
        }
        else
        {
            StopCoroutine("Fade");
            StartCoroutine("Fade");
        }
    }

    IEnumerator HighlightInteractable()
    {
        while (true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, maxDist, interactableMask);
            if (hit.transform && ((hit.transform.position - transform.position).magnitude <= interactionRange))
            {
                IInteractable newInteractable = hit.transform.GetComponent<IInteractable>();
                if(ActiveObject!=newInteractable)
                {
                    ActiveObject = newInteractable;
                }
            }
            else
            {
                ActiveObject = null;
            }
            yield return new WaitForSeconds(.1f);
        }
    }

    //DEBUG
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }

    private void LevelEnter()
    {
        StopAllCoroutines();
        StartCoroutine(HighlightInteractable());
    }
}
