using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
	public int damagePerHit = 20;
	public float timeBetweenAttacks = 0.25f;
	private GameObject attackableObject;

	float timer;
	int attackableMask;
	BoxCollider attackCollider;

	void Awake ()
	{
		attackableMask = LayerMask.GetMask ("Attackable");
		attackCollider = GetComponent<BoxCollider> ();
	}


	void Update ()
	{
		timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenAttacks && Time.timeScale != 0)
		{
			Attack ();
		}
			
	}

	void Attack ()
	{
		timer = 0f;

		//Start attack animation here
		//AttackAnimation();

		if(attackableObject != null){
			//Call takeDamage() method on attackableObject
		}
	}

	void OnTriggerEnter(Collider other){
		GameObject otherObj = other.gameObject;

		if(otherObj.layer == attackableMask && attackableObject == null)
			attackableObject = other.gameObject;
	}

	void OnTriggerExit(Collider other){
		GameObject otherObj = other.gameObject;

		//Our attackable object has left
		if(otherObj.GetInstanceID() == attackableObject.GetInstanceID())
			attackableObject = null;
	}

	void OnTriggerStay(Collider other){
		GameObject otherObj = other.gameObject;

		if(otherObj.layer == attackableMask && attackableObject == null)
			attackableObject = other.gameObject;
	}
}
