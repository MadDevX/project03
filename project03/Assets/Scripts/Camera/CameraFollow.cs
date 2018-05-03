using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float maxDistance = 100f;
    public float lerpFactor;
    public float rotateSpeed = 100f;
    public int fadeoutFrames = 20;
    [SerializeField] private Transform player;

    [SerializeField] private Vector3 offset;
    private Vector3 startPosition;
    private int fadingMask;
    //private int fadingLayer;
    //private int ignoreLayer;
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
        //fadingLayer = LayerMask.NameToLayer("Fading");
        //ignoreLayer = LayerMask.NameToLayer("Ignore Raycast");
        StartCoroutine(CheckObstacles());
	}

    private void Update()
    {
        //rotateInput = Input.GetAxis("Rotate") * rotateSpeed * Time.deltaTime;
        if(Input.GetMouseButtonDown(2) || Input.GetMouseButtonUp(2))
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
        MeshRenderer roof = null;
        while (player.transform)
        {
            if (Physics.Raycast(transform.position, player.position-transform.position, out hit, maxDistance, fadingMask))
            {
                roof = hit.transform.GetComponent<MeshRenderer>();
                roof.enabled = false;
                //roof.gameObject.layer = ignoreLayer;
                //Color c = roof.material.color;
                //c.a = 0;
                //roof.material.color = c;
            }
            else if(roof!=null)
            {
                roof.enabled = true;
                ///roof.gameObject.layer = fadingLayer;
                //Color c = roof.material.color;
                //c.a = 1;
                //roof.material.color = c;
                roof = null;
            }
            yield return new WaitForSeconds(.1f);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
