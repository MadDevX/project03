using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Helper;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public float dodgeDist;
    public float minDist;
    public float dodgeReset;
    public float slerpValue;
    //public Camera mainCamera;
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

    //private void Start()
    //{
    //    if (mainCamera == null)
    //    {
    //        mainCamera = Camera.main;
    //    }
    //    if (LevelLoader.Instance != null)
    //    {
    //        LevelLoader.Instance.loadFinished += LevelEnter;
    //    }
    //}

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
    }

    // Update is called once per frame
    void Update ()
    {
        dodgeTimer += Time.deltaTime;
        if (dodgeSlider != null)
        {
            UpdateDodgeSlider();
        }
    }

    public void Move()
    {
        Vector3 translationVector = movement * speed * Time.deltaTime;
        rb.MovePosition(rb.position + translationVector);
        //Debug.Log(rb.velocity.ToString());    
    }

    public void Dodge()
    {
        if (movement.magnitude != 0 && dodgeTimer >= dodgeReset)
        {
            rb.velocity = movement.normalized * speed * 10 / 6;
            dodgeTimer = 0f;
        }
    }

    public void Rotate()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
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


    //private void LevelEnter()
    //{
    //    mainCamera = Camera.main;
    //}
}
