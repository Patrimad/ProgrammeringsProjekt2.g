using UnityEngine;
using UnityEngine.AI;

public class EnemyNavAI : MonoBehaviour
{
    public Transform[] patrolPoints; // Waypoints til patrulje
    public float detectionRadius = 10f; // Radius for at opdage spilleren
    public float chaseSpeed = 5f;
    public float patrolSpeed = 2f;
    public float stopDistance = 1.5f;

    private NavMeshAgent agent;
    private int currentPatrolIndex;
    private Transform player;
    private bool chasingPlayer = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (patrolPoints.Length > 0)
        {
            agent.speed = patrolSpeed;
            agent.SetDestination(patrolPoints[0].position);
        }
    }

    void FixedUpdate()
    {
        Transform closestPlayer = GetClosestPlayer();

        if (closestPlayer != null && Vector3.Distance(transform.position, closestPlayer.position) <= detectionRadius)
        {
            // Start chasing the closest player within range
            chasingPlayer = true;
            agent.speed = chaseSpeed;
            Vector3 directionToPlayer = closestPlayer.position - transform.position;
            if (directionToPlayer.magnitude > stopDistance)
            {
                agent.SetDestination(closestPlayer.position);
            }
            else
            {
                agent.ResetPath();
            }
        }
        else
        {
            if (chasingPlayer)
            {
                chasingPlayer = false;
                agent.speed = patrolSpeed;
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);
            }

            Patrol();
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Tegner detection radius i editoren.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    Transform GetClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Transform closest = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject p in players)
        {
            float distance = Vector3.Distance(transform.position, p.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = p.transform;
            }
        }

        return closest;
    }

}