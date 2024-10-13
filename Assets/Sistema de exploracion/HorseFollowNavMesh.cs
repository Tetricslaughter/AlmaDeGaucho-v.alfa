using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HorseFollowNavMesh : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float stopDistance = 2f; // Distancia m�nima para detenerse
    public float maxDistance = 20f; // Distancia m�xima para correr
    public float walkSpeed = 3f; // Velocidad caminando
    public float trotSpeed = 6f; // Velocidad trotando
    public float runSpeed = 12f; // Velocidad corriendo

    private NavMeshAgent agent;
    private bool isFollowing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Obt�n el componente NavMeshAgent
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            isFollowing = !isFollowing; // Alternar seguimiento
        }

        if (isFollowing)
        {
            FollowPlayerWithNavMesh();
        }
        else
        {
            agent.isStopped = true; // Detener el movimiento si no est� siguiendo
        }
    }

    void FollowPlayerWithNavMesh()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= stopDistance)
        {
            agent.isStopped = true; // Detener el agente si est� cerca
            return;
        }
        else
        {
            agent.isStopped = false; // Permitir que el agente se mueva
        }

        // Determinar la velocidad seg�n la distancia
        if (distance > maxDistance * 0.75f)
        {
            agent.speed = runSpeed; // Correr si est� muy lejos
        }
        else if (distance > maxDistance * 0.5f)
        {
            agent.speed = trotSpeed; // Trotar si est� en una distancia media
        }
        else
        {
            agent.speed = walkSpeed; // Caminar si est� relativamente cerca
        }

        // Establecer el destino al jugador
        agent.SetDestination(player.position);
    }
}
