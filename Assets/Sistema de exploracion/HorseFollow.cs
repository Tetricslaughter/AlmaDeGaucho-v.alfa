using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseFollow : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float stopDistance = 2f; // Distancia mínima para detenerse
    public float maxDistance = 20f; // Distancia máxima para correr
    public float walkSpeed = 3f; // Velocidad caminando
    public float trotSpeed = 6f; // Velocidad trotando
    public float runSpeed = 12f; // Velocidad corriendo

    private bool isFollowing = false;
    private bool isCloseToPlayer = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            isFollowing = !isFollowing; // Alterna el seguimiento al presionar H
        }

        if (isFollowing)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= stopDistance)
        {
            // Si está cerca del jugador, el caballo se detiene
            isCloseToPlayer = true;
            return;
        }
        else
        {
            isCloseToPlayer = false;
        }

        // Determinar la velocidad según la distancia
        float speed;
        if (distance > maxDistance * 0.75f)
        {
            speed = runSpeed; // Correr si está muy lejos
        }
        else if (distance > maxDistance * 0.5f)
        {
            speed = trotSpeed; // Trotar si está en una distancia media
        }
        else
        {
            speed = walkSpeed; // Caminar si está relativamente cerca
        }

        // Moverse hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Hacer que el caballo mire hacia el jugador
        transform.LookAt(player);
    }
}
