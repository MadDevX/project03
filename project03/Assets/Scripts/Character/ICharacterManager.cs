using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterManager
{
    /// <summary>
    /// Function to be called when character's health reaches 0.
    /// </summary>
    void OnDeath();
}
