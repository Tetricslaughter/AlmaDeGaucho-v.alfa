using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehaviourScript : MonoBehaviour
{
    public string animalName;
    public float health;

    public float speed;
    public int meatAmount;
    public int leatherAmount;

    // Start is called before the first frame update
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        DropLoot();
        Destroy(gameObject);
    }

    private void DropLoot()
    {
        Debug.Log($"{animalName} Solto {meatAmount} carnes y {leatherAmount} cueros.");
        
    }
}
