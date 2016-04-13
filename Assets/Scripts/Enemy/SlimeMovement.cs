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
    NavMeshAgent nav;               // Reference to the nav mesh agent.

    PlayerMovement playerMove;
    public float attackRange = 3f;
    public float timerInterval = 1.5f;
    public float rangeBound = 10f;
    float timeLeft = 0f;

    bool canAttack;                 // Whether the slime is able to attack.
    int damage;                     // How much damage the slime does.
    int attackWaitPeriod;           // How long the slime will wait between attacks.

    public bool canMove;            // Used in WaveScript to determine if a wave can move

    void Awake()
    {
        game = Game.GetGame();

        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMove = player.GetComponent<PlayerMovement>();
        enemyHealth = GetComponent<SlimeHealth>();
        nav = GetComponent<NavMeshAgent>();

        nav.enabled = false;

        canAttack = true;
        damage = 2;
        attackWaitPeriod = 1000;

        canMove = false;
    }


    void Update()
    {
        // If the enemy and the player have health left...
        float distanceFromPlayer = Mathf.Pow(player.position.x - nav.nextPosition.x, 2) + Mathf.Pow(player.position.z - nav.nextPosition.z, 2);
        distanceFromPlayer = Mathf.Sqrt(distanceFromPlayer);

        if (canMove && !nav.enabled && distanceFromPlayer > attackRange)
            nav.enabled = true;

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
                    if (playerMove.isStealth) {
                        //nav.enabled = false;
                        RunAway();
                    }
                    //Player not stealth.
                    else
                    {
                        nav.SetDestination(player.position);
                        //Debug.Log("Chasing player.");
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
                    Attack();
                    //Debug.Log("Player in range and not stealth");
                    nav.enabled = false;
                }
            }
        }
    }

    void RunAway()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            if (!nav.enabled) nav.enabled = true;
            Vector3 nextPos;
            nextPos.x = nav.nextPosition.x + Random.Range(-1 * rangeBound, rangeBound);
            nextPos.z = nav.nextPosition.z + Random.Range(-1 * rangeBound, rangeBound);
            nextPos.y = nav.nextPosition.y;
            nav.SetDestination(nextPos);
            timeLeft = timerInterval;
        }
        
    }
    /// <summary>
    /// This triggers the slime to attack the character.
    /// Maybe this should be in an "attack script" or something
    /// but I didn't want to add a new script to every slime.
    /// </summary>
    void Attack()
    {
        if (canAttack)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            AttackWait();
        }
    }

    /// <summary>
    /// After attacking, the slime must wait temporarily before attacking again.
    /// </summary>
    void AttackWait()
    {
        canAttack = false;

        Timer t = new Timer();
        t.Elapsed += new ElapsedEventHandler(AttackWaitFinished);
        t.Interval = attackWaitPeriod;
        t.AutoReset = false;
        t.Start();
    }

    /// <summary>
    /// The slime is able to attack again after the set period of time.
    /// </summary>
    void AttackWaitFinished(object sender, ElapsedEventArgs args)
    {
        canAttack = true;
        ((Timer)sender).Dispose();
    }

}