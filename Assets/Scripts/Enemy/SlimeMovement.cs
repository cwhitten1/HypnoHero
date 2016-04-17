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
    GameObject environment;
    Animation anim; 				// Reference to this enemy's animations

    PlayerMovement playerMove;
    private const float minAttackRange = 3f;

    private float attackRange;
    public float rangeAdjustment = -.5f;
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

        // Set up the references.
        environment = GameObject.FindGameObjectWithTag("Environment");
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
        float slimeScaleFactor = environment.GetComponent<SlimeScaling>().scaleFactor;
        attackRange = minAttackRange * slimeScaleFactor + rangeAdjustment;
        Debug.Log("Setting new attack range: " + attackRange);

        float distanceFromPlayer = Mathf.Pow(player.position.x - nav.nextPosition.x, 2) + Mathf.Pow(player.position.z - nav.nextPosition.z, 2);
        distanceFromPlayer = Mathf.Sqrt(distanceFromPlayer);

        if (!nav.enabled && distanceFromPlayer > attackRange)
            SetNavEnabled(true);

        bool playerAlive = playerHealth.currentHealth > 0,
        mobAlive = enemyHealth.currentHealth > 0,
        outOfRange = distanceFromPlayer > attackRange;



        if (mobAlive && playerAlive)
        {
            //Player out of attack range.
            if (outOfRange)
            {
                //Or not attacking?
                if (nav.enabled)
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
                        //Debug.Log("Chasing player.");
                        nav.SetDestination(player.position);
                    }
                }
            }
            //Player in attack range.
            else
            {
                //Is stealth.
                if (playerMove.isStealth)
                {
                    //Debug.Log("Player in range and stealth");
                    RunAway();
                }

                //Player not stealth.
                else
                {
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

	public float GetAttackRange(){
		return attackRange;
	}
}