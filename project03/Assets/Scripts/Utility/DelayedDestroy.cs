using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestroy : MonoBehaviour {

    public float delay = 2f;
	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, delay);
	}
}
