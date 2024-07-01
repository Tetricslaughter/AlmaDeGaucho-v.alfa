using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaEnemigo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Da�o");
        }

        if (other.CompareTag(""))
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Bloqueo");
        }
    }
}
