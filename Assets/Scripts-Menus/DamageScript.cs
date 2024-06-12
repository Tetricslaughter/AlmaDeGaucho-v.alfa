using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public float damage;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        AnimalBehaviourScript animal = collision.gameObject.GetComponent<AnimalBehaviourScript>();
        if (animal != null) 
        {
            animal.TakeDamage(damage);
        }
    }

    // Update is called once per frame

}
