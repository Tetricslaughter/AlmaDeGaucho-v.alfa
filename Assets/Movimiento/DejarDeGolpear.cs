using SUPERCharacte;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DejarDeGolpear : MonoBehaviour
{
    public JuanMoveBehaviour script;
    public Rigidbody rb;
    public bool avanzaSolo;
    public float impulsoEsquivar = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (avanzaSolo)
        {
            rb.velocity = -transform.forward * impulsoEsquivar;
        }
    }
    void DetenerAtaque()
    {
        script.atacando=false;
        script.walkingSpeed = 125;
    }

    void AvanzaSolo()
    {
        avanzaSolo = true;
    }
    void DejaDeAvanzar()
    {
        avanzaSolo=false;
    }
}
