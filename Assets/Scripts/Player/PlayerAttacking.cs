using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
	public int damagePerHit = 50;
	public float timeBetweenAttacks = 0.25f;
	private GameObject attackableObject;

	float timer;
	int attackableMask;
	BoxCollider attackCollider;
	Animation anim;

	void Awake ()
	{
		attackableMask = LayerMask.NameToLayer ("Attackable");
		attackCollider = GetComponent<BoxCollider> ();
		attackableObject = null;

		anim = GetComponent<Animation> ();
		anim ["Attack"].layer = 1;
	}


	void Update ()
	{
		timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenAttacks && Time.timeScale != 0)
		{
			Attack ();
			Debug.LogWarning ("Attacking");
		}
			
	}

	void Attack ()
	{
		timer = 0f;

		//Start attack animation here
		anim.CrossFade("Attack");
        
		if(attackableObject != null){
            //attackableObject.GetComponent<SlimeHealth>().currentHealth -= 400000000;
            attackableObject.GetComponent<SlimeHealth>().TakeDamage (damagePerHit);
            
            //GameSystem.RestartLevel();

        }
	}

	void OnTriggerEnter(Collider other){
		GameObject otherObj = other.gameObject;
        //GameObject.Find("Player").GetComponent<PlayerHealth>().TakeDamage(1);
        if (otherObj.layer == attackableMask) {
			attackableObject = other.gameObject;
			//Debug.Log (attackableObject.name + " is in range");
		}
	}

	void OnTriggerExit(Collider other){
		GameObject otherObj = other.gameObject;

		//Our attackable object has left
		if (attackableObject != null && otherObj.GetInstanceID () == attackableObject.GetInstanceID ()) {
			//Debug.Log (attackableObject.name + " is now out of range");
			attackableObject = null;
		}
	}

	void OnTriggerStay(Collider other){
		GameObject otherObj = other.gameObject;

		if(otherObj.layer == attackableMask && attackableObject == null)
			attackableObject = other.gameObject;
	}
}
