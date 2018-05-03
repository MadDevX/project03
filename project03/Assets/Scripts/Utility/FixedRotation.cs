using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour {

    private RectTransform rectTransform;
    private Quaternion rotation;
    //private Transform parent;
    //private Vector3 offset;
	// Use this for initialization
	void Awake ()
    {
        //parent = transform.parent;
        rectTransform = GetComponent<RectTransform>();
        rotation = rectTransform.rotation;
        //offset = rectTransform.position - parent.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        rectTransform.rotation = rotation;
        //rectTransform.position = parent.position + offset;
	}
}
