using UnityEngine;
using System.Collections;

public class SlimeAttacking : EnemyAttacking
{
	Animation anim; 				// Reference to this enemy's animations
	Transform player;               // Reference to the player's position.
	PlayerHealth playerHealth;
	SlimeMovement slimeMovement;
	SlimeScaling slimeScaling; 

	public float attackArc = 60;         // How far away from forward this enemy can attack
	private float attackRange;
	private const float minAttackRange = 3f;
	public float rangeAdjustment = -.5f;




	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animation>();
		anim["Attack"].layer = 1;

		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerHealth = player.GetComponent<PlayerHealth>();
		slimeMovement = GetComponent<SlimeMovement> ();
		slimeScaling = GameObject.FindGameObjectWithTag("Environment").GetComponent<SlimeScaling> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float slimeScaleFactor = slimeScaling.scaleFactor;
		attackRange = minAttackRange * slimeScaleFactor + rangeAdjustment;

		//Debug.Log("Setting new attack range: " + attackRange);
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

		if (Mathf.Abs (angle) < attackArc && range < attackRange) {
			return true;
		} else
			return false;
	}

	public override bool CheckIfInAttackRange(){
		Vector3 directionToTarget = player.transform.position -transform.position;
		float range = directionToTarget.magnitude;

		if (range <= attackRange)
			return true;
		else
			return false;
	}

	public override bool CheckIfFacingPlayer(){
		Vector3 directionToTarget = player.transform.position -transform.position;
		float angle = Vector3.Angle (transform.forward, directionToTarget);

		if (Mathf.Abs (angle) < attackArc) {
			return true;
		} else
			return false;
	}
	


}

