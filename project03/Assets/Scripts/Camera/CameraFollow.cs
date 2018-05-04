using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FaderAsync;

public class CameraFollow : MonoBehaviour
{
    public float maxDistance = 100f;
    public float lerpFactor;
    public float rotateSpeed = 100f;
    public int fadeoutFrames = 15;
    [SerializeField] private Transform player;

    [SerializeField] private Vector3 offset;
    private Vector3 startPosition;
    private int fadingMask;
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
            StartCoroutine(CheckObstacles());
        }
    }

	// Use this for initialization
	void Start ()
    {
        startPosition = transform.position;
        fadingMask = LayerMask.GetMask("Fading");
        StartCoroutine(CheckObstacles());
	}

    private void Update()
    {
        //rotateInput = Input.GetAxis("Rotate") * rotateSpeed * Time.deltaTime;
        if(Input.GetButtonDown("Flip") || Input.GetButtonUp("Flip"))
        {
            offset = Quaternion.AngleAxis(180, Vector3.up) * offset;
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (player != null)
        {
            transform.position = Vector3.Lerp(transform.position, player.position + offset, lerpFactor);
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
                    //StopCoroutine(toFade.FadeIn(fadeoutFrames));
                    StartCoroutine(toFade.FadeOut(fadeoutFrames));
                    //StopCoroutine(fadingOut.FadeOut(fadeoutFrames));
                    StartCoroutine(fadingOut.FadeIn(fadeoutFrames));
                    fadingOut = toFade;
                }
            }
            else if(fadingOut!=null)
            {
                //StopCoroutine(fadingOut.FadeOut(fadeoutFrames));
                StartCoroutine(fadingOut.FadeIn(fadeoutFrames));
                fadingOut = null;
            }
            yield return new WaitForSeconds(.2f);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
