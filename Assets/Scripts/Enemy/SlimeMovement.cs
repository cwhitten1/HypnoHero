using UnityEngine;
using System.Timers;
using UnityEngine.UI;
using System.Collections;

public class SlimeMovement : MonoBehaviour
{
    Game game;

    Transform player;               // Reference to the player's position.
    PlayerHealth playerHealth;      // Reference to the player's health.
    SlimeHealth enemyHealth;        // Reference to this enemy's health.
	EnemyAttacking enemyAttacking;  // Reference to the slime's attacking script.
    NavMeshAgent nav;               // Reference to the nav mesh agent.
    GameObject stealthObject;

    Animation anim; 				// Reference to this enemy's animations

	PlayerMovement playerMove;
   
    public float timerInterval = 1.5f;
	public float rangeBound = 10f;
    float timeLeft = 0f;


	public float stunTime = 3; /// <summary>
	/// The number of seconds in which a slime is stunned
	/// before becoming reanimated.
	/// </summary>

    bool isStunned;

    void Awake()
    {
        game = Game.GetGame();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMove = player.GetComponent<PlayerMovement>();
        enemyHealth = GetComponent<SlimeHealth>();
		enemyAttacking = GetComponent<EnemyAttacking> ();
        anim = GetComponent<Animation>();

        nav = GetComponent<NavMeshAgent>();

        SetNavEnabled(false);

        isStunned = false;
    }


    void Update()
    {

		bool outOfRange = !enemyAttacking.CheckIfInAttackRange ();
		if (!nav.enabled && outOfRange)
            SetNavEnabled(true);

		bool playerAlive = playerHealth.currentHealth > 0,
		mobAlive = enemyHealth.currentHealth > 0;


        if (mobAlive && playerAlive)
        {
            //Player stealth.
            if (playerMove.isStealth)
            {
                stealthObject = GameObject.FindGameObjectWithTag("Player")
                    .GetComponent<PlayerMovement>()
                    .stealthObject;
                RunAway();
            }
            //Player not stealth.
            else
            {
				if (outOfRange) {
					//Debug.Log("Chasing player.");
					nav.SetDestination (player.position);
				} 
				else {
					enemyAttacking.Attack();
					//Debug.Log("Player in range and not stealth");
					SetNavEnabled(false);
				}
            }
        }
    }
    
    public void Stun()
    {
        NavMeshAgent nav = GetComponent<NavMeshAgent>();
        isStunned = true;
        SetNavEnabled(false);
        Invoke("UnStun", stunTime);
    }

    public void UnStun()
    {
        NavMeshAgent nav = GetComponent<NavMeshAgent>();
        isStunned = false;
        SetNavEnabled(true);
    }


    public void SetNavEnabled(bool enabled)
    {
        nav.enabled = true ? enabled && !isStunned : false;
        // nav cannot be enabled when stunned, but can always be disabled.
    }

    void RunAway()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            if (!nav.enabled) SetNavEnabled(true);
            bool isInBounds = true;
            Vector3 nextPos;
            while (isInBounds)
            {
                nextPos.x = nav.nextPosition.x + Random.Range(-1 * rangeBound, rangeBound);
                nextPos.z = nav.nextPosition.z + Random.Range(-1 * rangeBound, rangeBound);
                nextPos.y = nav.nextPosition.y;

                timeLeft = timerInterval;

                isInBounds = stealthObject.GetComponent<BoxCollider>().bounds.Contains(nextPos);
                if (!isInBounds)
                    nav.SetDestination(nextPos);
            }
        }

    }
}