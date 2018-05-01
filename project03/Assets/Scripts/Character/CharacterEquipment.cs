using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using Enums;

public class CharacterEquipment : MonoBehaviour {

    //public enum ItemType
    //{
    //    Weapon,
    //    Armor,
    //    Accessory
    //}
    [HideInInspector] public Weapon currentWeapon;
    [SerializeField] private Vector3 weaponAttach;
    [SerializeField] private Vector3 weaponRotation;
    [SerializeField] private Vector3 armorAttach;
    [SerializeField] private Vector3 armorRotation;
    [SerializeField] private Vector3 accessoryAttach;
    [SerializeField] private Vector3 accessoryRotation;
    private int interactableLayer;
    private int dynamicLayer;

    
	// Use this for initialization
	void Awake ()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
	}

    void Start()
    {
        interactableLayer = LayerMask.NameToLayer("Interactable");
        dynamicLayer = LayerMask.NameToLayer("Dynamic");
    }
    // Update is called once per frame
    void Update ()
    {
		
	}

    public void EquipItem(Item item)
    {
        switch(item.itemType)
        {
            case ItemType.Weapon:
                {
                    EquipWeapon(item);
                }
                break;
            default:
                break;
        }
    }



    //DO POSZCZEGOLNYCH TYPOW ITEMOW
    void EquipWeapon(Item item)
    {
        DetachWeapon(item);
        AttachItem(item, weaponAttach, weaponRotation);
        ConvertToEquipped(item);
        currentWeapon = item.GetComponent<Weapon>();
        if (tag == "Player")
        {
            item.gameObject.FindComponentInChildWithTag<LineRenderer>("BulletSpawn").enabled = true;
        }
    }

    /// <summary>
    /// Detaches weapon from character. If item is not null, the transforms of the current weapon and the item shall be swapped.
    /// </summary>
    /// <param name="item"></param>
    public void DetachWeapon(Item item=null)
    {
        if (currentWeapon != null)
        {
            currentWeapon.transform.parent = null;
            if (item != null)
            {
                currentWeapon.transform.position = item.transform.position + Vector3.up * .2f;
                currentWeapon.transform.rotation = item.transform.rotation;
            }
            ConvertToInteractable(currentWeapon);
            currentWeapon.gameObject.FindComponentInChildWithTag<LineRenderer>("BulletSpawn").enabled = false;
        }
    }

    void AttachItem(Item item, Vector3 attachPosition, Vector3 attachRotation)
    {
        item.transform.parent = transform;
        item.transform.localPosition = attachPosition;
        item.transform.localRotation = Quaternion.Euler(attachRotation);
    }

    /// <summary>
    /// Sets properties of an item to match those of an interactable objects: enables physics and sets appropriate layer.
    /// </summary>
    /// <param name="item"></param>
    void ConvertToInteractable(Item item)
    {
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.GetComponent<Collider>().enabled = true;
        item.gameObject.layer = interactableLayer;
    }

    /// <summary>
    /// Sets properties of an item preventing it from being interacted with: disables physics and sets appropriate layer.
    /// </summary>
    /// <param name="item"></param>
    void ConvertToEquipped(Item item)
    {
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.GetComponent<Collider>().enabled = false;
        item.gameObject.layer = dynamicLayer;
    }
}
