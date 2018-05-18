using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerMovement pMovement;
    [SerializeField] PlayerShooting pShooting;
    [SerializeField] PlayerAction pAction;

	void Update ()
    {
        if (!PauseMenu.GameIsPaused)
        {
            if (Input.GetButton("Fire1"))
            {
                pShooting.UseWeapon();
            }

            if (Input.GetButton("Jump"))
            {
                pMovement.Dodge();
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                pAction.InteractWith();
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                pAction.DetachWeapon();
            }
        }
    }
}
