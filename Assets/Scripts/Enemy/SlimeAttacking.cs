using UnityEngine;
using System.Collections;

public class SlimeAttacking : EnemyAttacking
{
	Animation anim; 				// Reference to this enemy's animations
	Transform player;               // Reference to the player's position.
	PlayerHealth playerHealth;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animation>();
		anim["Attack"].layer = 1;

		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerHealth = player.GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	protected override void doAttack ()
	{
			anim.CrossFade("Attack");

			float animationDuration = anim ["Attack"].length;
			Invoke ("DamagePlayer", animationDuration/2);
	}

	void DamagePlayer(){
		player.GetComponent<PlayerHealth>().TakeDamage(damage);
	}
}

