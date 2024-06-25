using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Niandus : AnimalBehaviourScript
{
    public NavMeshAgent agent;
    private Transform player;
    public float detectionRadius = 20f;
    public float fleeDistance = 10f; // Distancia a la que huirá el Niandus
    public float patrolRadius = 10f; // Radio de patrullaje
    private Vector3 initialPosition; // Posición inicial para el patrullaje

    private bool isFleeing = false; // Indica si el Niandus está huyendo actualmente

    // Start is called before the first frame update
    void Start()
    {
        animalName = "Niandus";
        health = 100f;
        speed = 5f;
        meatAmount = 10;
        leatherAmount = 5;
        // Aquí deberías asignar los prefabs de meatPrefab y leatherPrefab según tu diseño

        agent = GetComponent<NavMeshAgent>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found!");
        }

        initialPosition = transform.position;
        StartCoroutine("PatrolRoutine"); // Iniciar rutina de patrullaje
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || agent == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Si el jugador está dentro del radio de detección y el Niandus no está actualmente huyendo, huir
        if (distanceToPlayer < detectionRadius && !isFleeing)
        {
            isFleeing = true;
            Flee();
        }
        // Si el jugador está fuera del radio de detección, dejar de huir y patrullar
        else if (distanceToPlayer >= detectionRadius && isFleeing)
        {
            isFleeing = false;
        }

        // Si está huyendo y no ha escapado completamente del jugador, continuar huyendo
        if (isFleeing && distanceToPlayer < detectionRadius)
        {
            Flee();
        }
        // Si no está huyendo y no está patrullando, iniciar patrullaje
        else if (!isFleeing && !agent.pathPending && (agent.remainingDistance <= agent.stoppingDistance))
        {
            Patrol();
        }
    }

    private void Flee()
    {
        Vector3 fleeDirection = (transform.position - player.position).normalized;
        Vector3 newGoal = transform.position + fleeDirection * fleeDistance; // Punto de huida a 'fleeDistance' unidades de distancia del jugador

        NavMeshHit hit;
        if (NavMesh.SamplePosition(newGoal, out hit, fleeDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        else
        {
            Debug.LogWarning("No se pudo encontrar un punto de huida válido.");
        }
    }

    private void Patrol()
    {
        // Genera un nuevo destino aleatorio dentro del radio de patrullaje
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += initialPosition;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        else
        {
            Debug.LogWarning("No se pudo encontrar un punto de patrullaje válido.");
        }
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            // Espera aleatoria entre 5 y 10 segundos antes de cambiar de destino de patrullaje
            yield return new WaitForSeconds(Random.Range(5f, 10f));

            // Si no está huyendo, continuar patrullando
            if (!isFleeing)
            {
                Patrol();
            }
        }
    }

    // Draw the detection radius in the scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(initialPosition, patrolRadius);
    }
}
