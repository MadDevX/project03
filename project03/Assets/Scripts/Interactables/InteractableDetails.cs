using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Interactable", menuName = "Interactables/Interactable")]
public class InteractableDetails : ScriptableObject
{
    public new string name;
    public string description;

    public override string ToString()
    {
        return name + "\n\n" + description;
    }
}