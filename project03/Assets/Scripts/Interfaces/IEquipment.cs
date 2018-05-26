using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipment
{
    void Use(GameObject target=null);
    void Remove();
}
