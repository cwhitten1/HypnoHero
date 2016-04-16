using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
	public int damagePerHit = 50;
	public float timeBetweenAttacks = 0.25f;
	private GameObject attackableObject;

    float originalDamagePerHit;
    float confidenceMultiplier; /// <summary>
    /// The amount your damage will be multiplied by when you have full confidence.
    /// The damage will gradually increase the more confidence you get.
    /// </summary>

	float timer;
	int attackableMask;
	BoxCollider attackCollider;
	Animation anim;

    float flashlightRange;
    float flashlightIntensity;

	void Awake ()
	{
		attackableMask = LayerMask.NameToLayer ("Attackable");
		attackCollider = GetComponent<BoxCollider> ();
		attackableObject = null;

		anim = GetComponent<Animation> ();
		anim ["Attack"].layer = 2;
		anim ["Attack"].speed = anim ["Attack"].length / timeBetweenAttacks;


        confidenceMultiplier = 3;
        originalDamagePerHit = damagePerHit;

        flashlightRange = 100;
        flashlightIntensity = 3.4f;
	}


	void Update ()
	{
		timer += Time.deltaTime;

        // Damage increases the more confidence you have.
        damagePerHit = (int)(originalDamagePerHit * (1.0 + (confidenceMultiplier - 1) * (Game.GetGame().GetConfidence() / 100.0)));


        if (Input.GetButton ("Fire1") && timer >= timeBetweenAttacks && Time.timeScale != 0)
		{
			Attack ();
			Debug.LogWarning ("Attacking");
		}

        if (Input.GetButton("Fire2"))
        {
            FlashlightOn();

            Ray shootRay = new Ray(transform.position, transform.forward);
            RaycastHit shootHit;
            float range = flashlightRange;
            int shootableMask = LayerMask.GetMask("Attackable");
            
            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            { 
                SlimeMovement enemyMovement = shootHit.collider.GetComponent<SlimeMovement>();
                if (enemyMovement != null)
                {
                    enemyMovement.Stun();

                }
            }
        }
        else
        {
            FlashlightOff();
        }

    }

    void FlashlightOn()
    {
        GameObject.Find("Flashlight").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("FlashlightLight").GetComponent<Light>().intensity = flashlightIntensity;
    }
    
    void FlashlightOff()
    {
        GameObject.Find("Flashlight").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("FlashlightLight").GetComponent<Light>().intensity = 0;
    }

    void Attack ()
	{
		timer = 0f;

		//Start attack animation here
		anim.Stop (); 
		anim.Play ("Attack");

		if(attackableObject != null){
			Invoke("DamageAttackableObject", timeBetweenAttacks/2);
        }
	}

	void DamageAttackableObject()
	{
		attackableObject.GetComponent<SlimeHealth>().TakeDamage (damagePerHit);
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
