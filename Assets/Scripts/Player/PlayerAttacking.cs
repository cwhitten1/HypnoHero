using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
	public int damagePerHit = 0;
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
    public float flashlightBatteryDrainSpeed;/// <summary>
    /// percentage per second flashlight is being used
    /// </summary>


    void Awake ()
	{
		attackableMask = LayerMask.NameToLayer ("Attackable");
		attackCollider = GetComponent<BoxCollider> ();
		attackableObject = null;

		anim = GetComponent<Animation> ();
		anim ["Attack"].layer = 2;
		anim ["Attack"].speed = anim ["Attack"].length / timeBetweenAttacks;

        originalDamagePerHit = damagePerHit;

        flashlightRange = 100;
        flashlightIntensity = 3.4f;
	}


	void Update ()
    {
        Debug.Log(damagePerHit);
        timer += Time.deltaTime;
        int confidence = Game.GetGame().GetConfidence();

        // Damage increases the more confidence you have.
        damagePerHit = (int) System.Math.Ceiling(
            (originalDamagePerHit *
            (1 +  (confidence / 100.0))
            ));
        Debug.Log(damagePerHit);


        if (Input.GetButton ("Fire1") && timer >= timeBetweenAttacks && Time.timeScale != 0)
		{
			Attack ();
			//Debug.LogWarning ("Attacking");
		}

        if (Input.GetButton("Fire2") && Game.GetGame().GetBatteryLife() > 0)
        {
            FlashlightOn();
        }
        else
        {
            FlashlightOff();
        }

    }

    /// <summary>
    /// Makes a ray coming from the flashlight with an angle to the left or right (right positive,
    /// left negative), with an angle of 0 being a ray goint directly through the center of the light.
    /// </summary>
    /// <param name="angle"></param>
    private Ray MakeFlashlightRay(float angle)
    {
        GameObject flashlight = GameObject.Find("Flashlight");
        Vector3 flashlightPosition = new Vector3(flashlight.transform.position.x, transform.position.y, flashlight.transform.position.z);
        
        Ray ray = new Ray(flashlightPosition, Vector3.RotateTowards(transform.forward, transform.right, angle * (float)System.Math.PI / 180.0f, 1));
        if (angle == 0)
            ray = new Ray(flashlightPosition, transform.forward);
        return ray;

    }
    
    /// <summary>
    /// Stun enemies using a ray from the flashlight.
    /// </summary>
    /// <param name="ray"></param>
    private void FlashlightAttack(Ray ray)
    {
        RaycastHit shootHit;
        float range = 200;
        int shootableMask = LayerMask.GetMask("Attackable");
        Debug.DrawRay(ray.origin,ray.direction, Color.red);
        if (Physics.Raycast(ray, out shootHit, range, shootableMask))
        {
            SlimeMovement enemyMovement = shootHit.collider.GetComponent<SlimeMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.Stun();
            }
        }
    }

    void FlashlightOn()
    {
        GameObject.Find("Flashlight").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("FlashlightLight").GetComponent<Light>().intensity = flashlightIntensity;
        Game.GetGame().DrainBattery(Time.deltaTime*flashlightBatteryDrainSpeed);


        ///Shoot several rays to hit enemies with if they come into the light.
        FlashlightAttack(MakeFlashlightRay(-15));

        FlashlightAttack(MakeFlashlightRay(7));

        FlashlightAttack(MakeFlashlightRay(0));

        FlashlightAttack(MakeFlashlightRay(7));

        FlashlightAttack(MakeFlashlightRay(15));
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
        Debug.Log(damagePerHit);
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
