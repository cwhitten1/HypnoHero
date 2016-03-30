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
    bool canAttack;                 // Whether the slime is able to attack.
    int damage;                     // How much damage the slime does.
    int attackWaitPeriod;           // How long the slime will wait between attacks.


    void Awake()
    {
        game = Game.GetGame();

        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<SlimeHealth>();
        nav = GetComponent<NavMeshAgent>();

        canAttack = true;
        damage = 2;
        attackWaitPeriod = 1000;
    }


    void Update()
    {
        // If the enemy and the player have health left...
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {

            //canAttack = false;
            var heading = player.position - gameObject.transform.position;
            var distance = heading.magnitude;
            //canAttack = true;
            if (distance < 4)
            {
                Attack();
            }

            // ... set the destination of the nav mesh agent to the player.
            nav.SetDestination(player.position);
        }
        // Otherwise...
        else
        {
            // ... disable the nav mesh agent.
            nav.enabled = false;
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