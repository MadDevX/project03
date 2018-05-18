using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjectRotation : MonoBehaviour {

    public Transform player;

	//Minimap Player rotation indicator
	void LateUpdate ()
    {
        if (player != null)
        {
            transform.rotation = Quaternion.Euler(0, 0, -player.rotation.eulerAngles.y);
        }
	}
}
