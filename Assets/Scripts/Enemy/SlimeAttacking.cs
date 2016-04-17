using UnityEngine;
using System.Collections;

public class SlimeAttacking : EnemyAttacking
{
	Animation anim; 				// Reference to this enemy's animations
	Transform player;               // Reference to the player's position.
	PlayerHealth playerHealth;
	SlimeMovement slimeMovement;

	public float attackArc = 60;         // How far away from forward this enemy can attack

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animation>();
		anim["Attack"].layer = 1;

		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerHealth = player.GetComponent<PlayerHealth>();
		slimeMovement = GetComponent<SlimeMovement> ();
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
		if(checkIfFacingPlayerAndInAttackRange())
			player.GetComponent<PlayerHealth>().TakeDamage(damage);
	}

	bool checkIfFacingPlayerAndInAttackRange(){
		Vector3 directionToTarget = player.transform.position -transform.position;
		float angle = Vector3.Angle (transform.forward, directionToTarget);
		float range = directionToTarget.magnitude;

		if (Mathf.Abs (angle) < attackArc && range < slimeMovement.GetAttackRange ()) {
			return true;
		} else
			return false;
	}
}

