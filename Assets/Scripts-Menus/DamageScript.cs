using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public float damage;

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        // Intenta obtener el componente EntityBehaviourScript o cualquiera de sus derivados
        EntityBehaviourScript entity = collision.gameObject.GetComponent<EntityBehaviourScript>();
        if (entity != null)
        {
            entity.TakeDamage(damage);
        }
    }
}
