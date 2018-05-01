using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Helper;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public float dodgeDist;
    public float minDist;
    public float dodgeReset;
    public float slerpValue;
    public Camera mainCamera;
    public Slider dodgeSlider;

    private float camRayLength = 100f;
    private float timer;
    private float dodgeTimer;
    private int groundMask;
    private Rigidbody rb;
    private Vector3 movement;
    private Vector3 dodgeDir;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        groundMask = LayerMask.GetMask("Ground");
        dodgeTimer = dodgeReset;
        if (dodgeSlider != null)
        {
            dodgeSlider.maxValue = dodgeReset;
            dodgeSlider.value = dodgeTimer;
        }
	}

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }
        Move();
        Rotate();
    }

    // Update is called once per frame
    void Update ()
    {
        Dodge();
        if (dodgeSlider != null)
        {
            UpdateDodgeSlider();
        }
    }

    void Move()
    {
        Vector3 translationVector = movement * speed * Time.deltaTime;
        rb.MovePosition(rb.position + translationVector);
    }

    void Dodge()
    {
        if(Input.GetButton("Jump") && dodgeTimer>=dodgeReset)
        {
            rb.velocity = movement.normalized * speed*10/6;
            dodgeTimer = 0f;
        }
        dodgeTimer += Time.deltaTime;
    }

    void Rotate()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit groundHit;
        if (Physics.Raycast(cameraRay, out groundHit, camRayLength, groundMask))
        {
            Vector3 rotationVector = groundHit.point - transform.position;
            rotationVector.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(rotationVector);
            rb.MoveRotation(newRotation);
        }
    }

    void UpdateDodgeSlider()
    {
        dodgeSlider.value = dodgeTimer;
    }
}
