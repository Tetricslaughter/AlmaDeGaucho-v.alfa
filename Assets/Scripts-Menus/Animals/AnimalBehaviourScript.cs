using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalBehaviourScript : MonoBehaviour
{
    public string animalName;
    public float health;

    public float speed;
    public int meatAmount;
    public int leatherAmount;
    public GameObject meatPrefab;
    public GameObject leatherPrefab;


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
        if (meatPrefab != null)
        {
            GameObject meat = Instantiate(meatPrefab, transform.position, Quaternion.identity);
            //LootScript meatLoot = meat.GetComponent<LootScript>();
            //meatLoot.amount = meatAmount;
            //meatLoot.lootName = "Meats";
        }

        if (leatherPrefab != null)
        {
            GameObject leather = Instantiate(leatherPrefab, transform.position, Quaternion.identity);
            //LootScript leatherLoot = leather.GetComponent<LootScript>();
            //leatherLoot.amount = leatherAmount;
            //leatherLoot.lootName = "Leathers";
        }
    }
}
