using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjectRotation : MonoBehaviour {

    public Transform player;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if (player != null)
        {
            transform.rotation = Quaternion.Euler(0, 0, -player.rotation.eulerAngles.y);
        }
	}
}
