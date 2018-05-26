using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement pMovement;
    private CharacterEquipment pEquipment;
    private PlayerAction pAction;

    private void Awake()
    {
        pMovement = GetComponent<PlayerMovement>();
        pEquipment = GetComponent<CharacterEquipment>();
        pAction = GetComponent<PlayerAction>();
    }

    private void FixedUpdate()
    {
        pMovement.Move();

        pMovement.Rotate();
    }

	void Update ()
    {
        if (!PauseMenu.GameIsPaused && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetButton("Jump"))
            {
                pMovement.Dodge();
            }

            if (Input.GetButton("Fire1"))
            {
                pEquipment.UseWeapon();
            }

            if (Input.GetButtonUp("Interact"))
            {
                pAction.InteractWith();
            }

            if (Input.GetButtonDown("Drop"))
            {
                //
            }
        }
    }

}
