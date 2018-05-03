using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRelativeUI : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        rectTransform.rotation = Quaternion.LookRotation(Vector3.down, cameraForward);
    }
}
