using UnityEngine;
using System.Collections;

public class SlimeMovement : MonoBehaviour
{
    Transform player;               // Reference to the player's position.
    PlayerHealth playerHealth;      // Reference to the player's health.
    SlimeHealth enemyHealth;        // Reference to this enemy's health.
    NavMeshAgent nav;               // Reference to the nav mesh agent.
    PlayerMovement playerMove;
    public float attackRange = 3f;
    public float timerInterval = 1.5f;
    public float rangeBound = 10f;
    float timeLeft = 0f;

    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMove = player.GetComponent<PlayerMovement>();
        enemyHealth = GetComponent<SlimeHealth>();
        nav = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        // If the enemy and the player have health left...
        float distanceFromPlayer = Mathf.Pow(player.position.x - nav.nextPosition.x, 2) + Mathf.Pow(player.position.z - nav.nextPosition.z, 2);
        distanceFromPlayer = Mathf.Sqrt(distanceFromPlayer);

        if (!nav.enabled && distanceFromPlayer > attackRange)
            nav.enabled = true;

        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0 && distanceFromPlayer > attackRange)
        {

            //Debug.Log("Player out of range.");
            // ... set the destination of the nav mesh agent to the player.
            if (nav.enabled && !playerMove.isStealth)
            {
                nav.SetDestination(player.position);
                Debug.Log("Chasing player.");
            }
            if (nav.enabled && playerMove.isStealth)
            {
                //Debug.Log("Player is out of range and stealth.");
                //nav.enabled = false;
                RunAway();
            }
        }
        else if (distanceFromPlayer <= attackRange)
        {
            if (playerMove.isStealth)
            {
                Debug.Log("Player in range and stealth");
                RunAway();
            }
            else
            {
                Debug.Log("Player in range and not stealth");
                nav.enabled = false;
            }
        }
    }

    void RunAway()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Vector3 nextPos;
            nextPos.x = nav.nextPosition.x + Random.Range(-1 * rangeBound, rangeBound);
            nextPos.z = nav.nextPosition.z + Random.Range(-1 * rangeBound, rangeBound);
            nextPos.y = nav.nextPosition.y;
            nav.SetDestination(nextPos);
            timeLeft = timerInterval;
        }
    }
}