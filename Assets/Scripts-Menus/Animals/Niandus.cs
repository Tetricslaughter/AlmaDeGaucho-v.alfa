using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Niandus : AnimalBehaviourScript
{
    public NavMeshAgent agent;
    private Transform player;
    private float detectionRadius = 20f;

    // Start is called before the first frame update
    void Start()
    {
        animalName = "Niandus";
        health = 100f;
        speed = 5f;
        meatAmount = 10;
        leatherAmount = 5;

        agent = GetComponent<NavMeshAgent>();
        /*if (agent == null)
        {
            Debug.Log("El NavMeshAgent no se encontró.");
        }
        else
        {
            agent.speed = speed;
        }*/
        agent.speed = speed;

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("El jugador no fue encontrado correctamente.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || agent == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRadius)
        {
            Flee();
        }
        else
        {
            Patrol();
        }
    }

    private void Flee()
    {
        Vector3 fleeDirection = (transform.position - player.position).normalized;
        Vector3 newGoal = transform.position + fleeDirection * detectionRadius;
        agent.SetDestination(newGoal);
    }

    private void Patrol()
    {
        if (!agent.hasPath)
        {
            Vector3 randomDirection = Random.insideUnitSphere * detectionRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, detectionRadius, 1);
            agent.SetDestination(hit.position);
        }
    }
}
