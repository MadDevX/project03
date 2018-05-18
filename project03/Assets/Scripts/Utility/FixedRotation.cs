using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour {

    private RectTransform rectTransform;
    private Quaternion rotation;

	void Awake ()
    {
        rectTransform = GetComponent<RectTransform>();
        rotation = rectTransform.rotation;
	}

	void Update ()
    {
        rectTransform.rotation = rotation;
	}
}
