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
    private int groundMask;
    private Rigidbody rb;
    private Vector3 movement;
    private float timer;
    private float dodgeTimer;
    private bool isFlipped;

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
        isFlipped = false;
	}

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (isFlipped)
        {
            movement = new Vector3(-moveHorizontal, 0.0f, -moveVertical);
        }
        else
        {
            movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        }
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
        if (movement.magnitude!=0 && Input.GetButton("Jump") && dodgeTimer >= dodgeReset)
        {
            Dodge();
        }
        dodgeTimer += Time.deltaTime;
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
            rb.velocity = movement.normalized * speed*10/6;
            dodgeTimer = 0f;
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

    public void Flip()
    {
        isFlipped = !isFlipped;
    }
}
