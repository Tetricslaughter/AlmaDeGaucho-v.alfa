using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseMovement : MonoBehaviour
{
    public float mouseHorizontal = 3f;
    public float mouseVertical = 2f;

    //public Animator anim;

    float h_mouse;
    float v_mouse;

    public float moveSpeed = 3.0f;
    public float runSpeed = 10.0f;
    public float runDuration = 30.0f; // Duración del tiempo que el caballo puede correr
    public float cooldownDuration = 10.0f; // Tiempo que debe caminar antes de poder correr de nuevo
    private float runTimer;
    private float cooldownTimer;
    private bool isRunning;

    float h;
    float v;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        runTimer = 0.0f;
        cooldownTimer = 0.0f;
        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        h_mouse = mouseHorizontal * Input.GetAxis("Mouse X");
        v_mouse = mouseVertical * Input.GetAxis("Mouse Y");

        transform.Rotate(0, h_mouse, 0);
        //Cam.transform.Rotate(-v_mouse, 0, 0);

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h, 0, v);

        if (Input.GetButton("Fire3") && cooldownTimer <= 0)
        {
            isRunning = true;
            runTimer = runDuration;
            cooldownTimer = cooldownDuration;

        }
        if (isRunning)
        {
            transform.Translate(direction * runSpeed * Time.deltaTime);
            runTimer -= Time.deltaTime;

            if (runTimer <= 0)
            {
                isRunning = false;
            }
        }    
        else
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            cooldownTimer -= Time.deltaTime;
            //anim.SetBool("walk", h != 0 || v != 0);
            //anim.SetBool("walk", true);
        }
            
        
    }
}
