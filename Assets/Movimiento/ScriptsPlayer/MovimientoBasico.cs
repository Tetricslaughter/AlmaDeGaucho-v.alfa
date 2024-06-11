using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoBasico : MonoBehaviour
{
    public float speed = 5f;
    private float speedRotate = 200f;
    private Animator animacion;
    private Rigidbody physicBody;
    // Start is called before the first frame update
    void Start()
    {
        animacion = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        transform.forward = Camera.main.transform.forward;
    }

    public void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontal, 0.0f, vertical) * Time.deltaTime * speed);

        float rotationY = (Input.GetAxis("Mouse X"));
        transform.Rotate(new Vector3(0, rotationY, 0) * Time.deltaTime * speedRotate);
        animacion.SetFloat("VelX", horizontal);
        animacion.SetFloat("VelY", vertical);

    }
}
