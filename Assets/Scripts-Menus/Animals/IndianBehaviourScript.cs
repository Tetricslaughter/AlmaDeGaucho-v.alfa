using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IndianBehaviourScript : EnemyBehaviourScript
{
    private Transform player;
    public float detectionRadius = 20f;
    public float attackRadius = 2f; // rango de ataque
    public float stopDistance = 3f; // distancia a la que se detendrá antes de atacar
    public float attackDamage = 10f; // daño
    public float attackCooldown = 1f; // tiempo de espera de ataques

    private bool isAttacking = false;
    private float lastAttackTime;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found!");
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

        // dentro del radio de detección se acerca
        if (distanceToPlayer < detectionRadius && distanceToPlayer > stopDistance)
        {
            MoveTowardsPlayer();
        }
        else if (distanceToPlayer <= stopDistance)// se detiene a cierta distancia del jugador y ataca
        {
            agent.isStopped = true;
            if (distanceToPlayer <= attackRadius && !isAttacking)
            {
                isAttacking = true;
                StartCoroutine(AttackPlayer());
            }
        }
        else
        {
            // se detiene si el jugador está fuera del rango de detección
            agent.isStopped = true;
        }
    }

    private void MoveTowardsPlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    private IEnumerator AttackPlayer()
    {
        while (isAttacking)
        {
            if (Time.time > lastAttackTime + attackCooldown)
            {
                lastAttackTime = Time.time;
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(attackDamage);
                }
            }
            yield return null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
