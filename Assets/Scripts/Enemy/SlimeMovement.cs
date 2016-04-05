using UnityEngine;
using System.Collections;

public class SlimeMovement : MonoBehaviour
{
	Transform player;               // Reference to the player's position.
	PlayerHealth playerHealth;      // Reference to the player's health.
	SlimeHealth enemyHealth;        // Reference to this enemy's health.
	NavMeshAgent nav;               // Reference to the nav mesh agent.
	public float attackRange = 3f;


	void Awake()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerHealth = player.GetComponent<PlayerHealth>();
		enemyHealth = GetComponent<SlimeHealth>();
		nav = GetComponent<NavMeshAgent>();
	}


	void Update()
	{
		// If the enemy and the player have health left...
		float distanceFromPlayer = Mathf.Pow(player.position.x - nav.nextPosition.x, 2) + Mathf.Pow(player.position.z - nav.nextPosition.z, 2);
		distanceFromPlayer = Mathf.Sqrt (distanceFromPlayer);

		if (!nav.enabled && distanceFromPlayer > attackRange)
			nav.enabled = true;

		if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0  && distanceFromPlayer > attackRange)
		{

			System.Console.WriteLine ("Monster: " + nav.nextPosition);
			System.Console.WriteLine ("Player: " + player.position);
			System.Console.WriteLine ("Distance: " + distanceFromPlayer);

			// ... set the destination of the nav mesh agent to the player.
			if (nav.enabled)
				nav.SetDestination(player.position);
		}
		// Otherwise...
		else
		{
			// ... disable the nav mesh agent.
			nav.enabled = false;
		}
	}
}