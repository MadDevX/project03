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
    public int fadeoutFrames = 15;
    [SerializeField] private Transform player;

    [SerializeField] private Vector3 offset;
    private Vector3 startPosition;
    private PlayerMovement playerControls;
    private int fadingMask;
    private int groundMask;
    private float rotateInput;

    public Transform Player
    {
        get
        {
            return player;
        }

        set
        {
            player = value;
            playerControls = player.GetComponent<PlayerMovement>();
            StartCoroutine(CheckObstacles());
        }
    }

	// Use this for initialization
	void Start ()
    {
        startPosition = transform.position;
        fadingMask = LayerMask.GetMask("Fading");
        groundMask = LayerMask.GetMask("Ground");
        StartCoroutine(CheckObstacles());
        playerControls = player.GetComponent<PlayerMovement>();
	}

    private void Update()
    {
        //rotateInput = Input.GetAxis("Rotate") * rotateSpeed * Time.deltaTime;
        if(Input.GetButtonDown("Flip"))
        {
            StartCoroutine("FlipCamera");
            playerControls.Flip();
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (player != null)
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(camRay, out hit, maxDistance, groundMask))
            {
                Vector3 targetPosition = (2*player.position + hit.point)/3;
                float f;
                if((f=(targetPosition-player.position).magnitude)>maxCameraDrift)
                {
                    targetPosition += (player.position - targetPosition).normalized * (f - maxCameraDrift);
                }
                targetPosition.y = 0;
                transform.position = Vector3.Lerp(transform.position, targetPosition + offset, lerpFactor);
            }
            else
            {
                
                transform.position = Vector3.Lerp(transform.position, player.position + offset, lerpFactor);
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, lerpFactor);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-offset, Vector3.up), lerpFactor);
    }

    IEnumerator CheckObstacles()
    {
        RaycastHit hit;
        MeshRenderer fadingOut = null;
        while (player.transform)
        {
            if (Physics.Raycast(transform.position, player.position-transform.position, out hit, maxDistance, fadingMask))
            {
                MeshRenderer toFade = hit.transform.GetComponent<MeshRenderer>();
                if (fadingOut != toFade)
                {
                    StartCoroutine(toFade.FadeOut(fadeoutFrames));
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
