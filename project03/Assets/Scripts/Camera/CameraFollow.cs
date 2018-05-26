using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FaderAsync;

public class CameraFollow : MonoBehaviour
{
    public float maxDistance = 100f;
    public float maxCameraDrift = 15f;
    public float lerpFactor;
    public float rotateSpeed = 100f;
    public float fadeAlpha = 0.1f;
    public int fadeoutFrames = 15;

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    private AudioListener cameraListener;
    private Vector3 startPosition;
    private PlayerMovement playerControls;
    private int fadingMask;
    private int groundMask;
    //private float rotateInput;

    public Transform Target
    {
        get
        {
            return target;
        }

        set
        {
            StopCoroutine("CheckObstacles");
            target = value;
            playerControls = target.GetComponent<PlayerMovement>();
            StartCoroutine(CheckObstacles());
        }
    }

	// Use this for initialization
	void Start ()
    {
        startPosition = transform.position;
        cameraListener = GetComponentInChildren<AudioListener>();
        cameraListener.transform.SetPositionAndRotation(transform.position - offset, Quaternion.LookRotation(Vector3.forward, Vector3.up));
        fadingMask = LayerMask.GetMask("Fading");
        groundMask = LayerMask.GetMask("Ground");
        StartCoroutine(CheckObstacles());
        playerControls = Target.GetComponent<PlayerMovement>();
	}

    private void Update()
    {
        if(Input.GetButtonDown("Flip"))
        {
            StartCoroutine("FlipCamera");
            playerControls.Flip();
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (Target != null)
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(camRay, out hit, maxDistance, groundMask))
            {
                Vector3 targetPosition = (2.0f*Target.position + hit.point)/3.0f;
                float f;
                if((f=(targetPosition-Target.position).magnitude)>maxCameraDrift)
                {
                    targetPosition += (Target.position - targetPosition).normalized * (f - maxCameraDrift);
                }
                targetPosition.y = 0f;
                transform.position = Vector3.Lerp(transform.position, targetPosition + offset, lerpFactor);
            }
            else
            {
                
                transform.position = Vector3.Lerp(transform.position, Target.position + offset, lerpFactor);
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, lerpFactor);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-offset, Vector3.up), lerpFactor);
    }

    //DO ZMIANY
    IEnumerator CheckObstacles()
    {
        RaycastHit hit;
        MeshRenderer fadingOut = null;
        while (Target)
        {
            if (Physics.Raycast(transform.position, Target.position-transform.position, out hit, maxDistance, fadingMask))
            {
                MeshRenderer toFade = hit.transform.GetComponent<MeshRenderer>();
                if (fadingOut != toFade)
                {
                    StartCoroutine(toFade.FadeOut(fadeoutFrames, fadeAlpha));
                    StartCoroutine(fadingOut.FadeIn(fadeoutFrames));
                    fadingOut = toFade;
                }
            }
            else if(fadingOut!=null)
            {
                StartCoroutine(fadingOut.FadeIn(fadeoutFrames));
                fadingOut = null;
            }
            yield return new WaitForSeconds(.2f);
        }

    }

    IEnumerator FlipCamera()
    {
        offset = Quaternion.AngleAxis(90, Vector3.up) * offset;
        yield return new WaitForSeconds(Time.deltaTime);
        offset = Quaternion.AngleAxis(90, Vector3.up) * offset;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
