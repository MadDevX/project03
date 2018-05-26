using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public abstract class EquipmentDetails : ItemDetails
{
    public EquipmentType type;
    public GameObject equipModel;

    public abstract GameObject CreateEquipped(out IEquipment equipment);

    public override string ToString()
    {
        return base.ToString() + "\n\nType: " + type.ToString();
    }
}
