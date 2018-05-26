using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using Enums;

public class CharacterEquipment : MonoBehaviour {


    [System.Serializable]
    public class AttachmentPoint
    {
        public EquipmentType type;
        public Vector3 position;
        public Vector3 rotation;
        public IEquipment currentItem;
    }

    public List<AttachmentPoint> attachmentPoints;
    private EquipmentDetails[] currentEquipment;
    private CharacterInventory inventory;

    
	// Use this for initialization
	void Awake ()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentType)).Length;
        currentEquipment = new EquipmentDetails[numSlots];
        inventory = GetComponent<CharacterInventory>();
	}

    public void EquipItem(EquipmentDetails item)
    {
        if(currentEquipment[(int)item.type]!=null)
        {
            inventory.Add(currentEquipment[(int)item.type]);
        }
        currentEquipment[(int)item.type] = item;
        inventory.Remove(item);
        foreach(AttachmentPoint p in attachmentPoints)
        {
            if(p.type==item.type)
            {
                if(p.currentItem!=null)
                {
                    p.currentItem.Remove();
                }
                GameObject model = item.CreateEquipped(out p.currentItem);
                AttachModel(model, p.position, p.rotation);
                break;
            }
        }
    }
    
    void AttachModel(GameObject item, Vector3 attachPosition, Vector3 attachRotation)
    {
        item.transform.parent = transform;
        item.transform.localPosition = attachPosition;
        item.transform.localRotation = Quaternion.Euler(attachRotation);
    }

    public void UseWeapon()
    {
        IEquipment weapon = attachmentPoints[(int)EquipmentType.Weapon].currentItem;
        if (weapon != null)
        {
            weapon.Use();
        }
    }
}
