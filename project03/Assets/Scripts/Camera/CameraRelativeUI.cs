using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRelativeUI : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector3 cameraForward;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        cameraForward = Vector3.zero;
    }

    private void Update()
    {
        cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        rectTransform.SetPositionAndRotation(rectTransform.position, Quaternion.LookRotation(Vector3.down, cameraForward));
        //rectTransform.rotation = Quaternion.LookRotation(Vector3.down, cameraForward);
    }
}
