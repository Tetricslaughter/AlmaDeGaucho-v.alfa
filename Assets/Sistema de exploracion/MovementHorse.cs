using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHorse : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float trotSpeed = 6f;
    public float runSpeed = 12f;
    public float turnSpeed = 200f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private Animator animator;

    private float currentSpeed;
    private bool isGalloping = false;
    private bool isRunning = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        HandleMovement();
        HandleSpeedChange();
    }

    void HandleMovement()
    {
        float moveDirection = Input.GetAxis("Vertical");
        float turnDirection = Input.GetAxis("Horizontal");

        // Movimiento hacia adelante y atrás
        Vector3 move = transform.forward * moveDirection * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + move);

        // Rotación del caballo
        if (turnDirection != 0)
        {
            Quaternion turn = Quaternion.Euler(0, turnDirection * turnSpeed * Time.deltaTime, 0);
            rb.MoveRotation(rb.rotation * turn);
        }

        // Salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Animaciones (asume que tienes un Animator configurado)
        //animator.SetFloat("Vel", Mathf.Abs(moveDirection));
        animator.SetFloat("Vel", moveDirection * currentSpeed);
    }

    void HandleSpeedChange()
    {
        // Aumentar velocidad con Shift
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isGalloping)
            {
                currentSpeed = trotSpeed;
                isGalloping = true;
            }
            else if (!isRunning)
            {
                currentSpeed = runSpeed;
                isRunning = true;
            }
        }

        // Reducir velocidad con Ctrl
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (isRunning)
            {
                currentSpeed = trotSpeed;
                isRunning = false;
            }
            else if (isGalloping)
            {
                currentSpeed = walkSpeed;
                isGalloping = false;
            }
        }
    }
}
