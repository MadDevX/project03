using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// Allows interaction with an interactable object.
    /// </summary>
    /// <param name="equip">Reference to character's equipment component.</param>
    /// <returns>True if interaction was a success.</returns>
    bool Interact(CharacterEquipment equip);
    void Highlight();
    void StopHighlight();
}
