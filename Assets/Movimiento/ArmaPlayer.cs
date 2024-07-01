using SUPERCharacte;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaPlayer : MonoBehaviour
{
    public GameObject facon;
    public BoxCollider faconCollider;
    private JuanMoveBehaviour moveBehaviour;

    public GameObject poncho;
    public BoxCollider ponchoCollider;
    //private bool tengoArma;
    private bool armaEquipada;
    public bool pressEquiparFacon;
    public bool pressManos;
    public bool pressRifle;
    //private Animator animator;

    
    // Start is called before the first frame update
    void Start()
    {
        moveBehaviour = GetComponent<JuanMoveBehaviour>();
        //animator = GetComponent<Animator>();
        DesactivarColliderArmas();//se desactivan los boxColliders del puño y de la espada
        //tengoArma = false;      //al iniciar la escena el player no tiene un arma
        armaEquipada = false;   //por lo tanto tampoco la tendria equipada
        pressEquiparFacon = false;
        pressManos = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pressEquiparFacon)
        {
            pressEquiparFacon= false;
            ActivateArma();
        }
        if (pressManos)
        {
            pressManos= false;
            DesactivarArma();
        }
        if (pressRifle)
        {
            pressRifle= false;
            moveBehaviour.tengoRifle =true;
        }

        //este if sirve para equiparse y desaquiparse el arma cuando se apreta la tecla especificada
        if (Input.GetKey(KeyCode.E)) //también como condición tiene que antes se tendria que haber agarrado un arma
        {
            if (armaEquipada)       //evalúa si tiene o no equipada el arma
            {
                //DesactivarArma();
            }
            else
            {
                //ActivateArma();
            }
        }
    }
    public void DesactivarColliderArmas()
    {
        if (faconCollider != null)
        {
            faconCollider.enabled = false;
        }
        if (ponchoCollider != null)
        {
            ponchoCollider.enabled = false;
        }
        //punioBoxCollider.enabled = false;
    }

    //Activa los colliders del puño y de la espada
    public void ActivarColliderArmas()
    {
        if (moveBehaviour.tengoFacon)
        {
            if (faconCollider != null)
            {
                faconCollider.enabled = true;
            }
            if (ponchoCollider != null)
            {
                ponchoCollider.enabled = true;
            }
        }
        //else
        //{
        //    punioBoxCollider.enabled = true;
        //}

    }

    public void ActivateArma()
    {
        //tengoArma = true;
        facon.SetActive(true);   //activa el arma del player
        poncho.SetActive(true);
        armaEquipada = true;
        //animator.SetBool("armaEquipada", true);
        moveBehaviour.tengoFacon = true; //cambia el estado de una variable correspondiente al Script PlayerBehaviour
        moveBehaviour.tengoRifle=false;
    }

    public void DesactivarArma()
    {
        facon.SetActive(false);              //desactiva el arma del player
        poncho.SetActive(false );
        armaEquipada = false;
        //animator.SetBool("armaEquipada", false);
        moveBehaviour.tengoFacon = false;
        moveBehaviour.tengoRifle = false;
    }
}
