using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

internal class SlimeHealth : MonoBehaviour
{
    public static int startingHealth = 100; //Say "Hello" to my little friend!
    public int currentHealth;
    //public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
	public float sinkSpeed = 0.2f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	Animation anim; 				// Reference to this enemy's animations
 
	Collider[] colliders;
    AudioSource slimeAudio;
    SlimeMovement slimeMovement;
    //SlimeShooting slimeShooting;
    bool isDead;
    bool damaged;
	bool isSinking;

    Game game;

    void Awake()
    {
        game = Game.GetGame();

        slimeAudio = GetComponent<AudioSource>();
        slimeMovement = GetComponent<SlimeMovement>();
		anim = GetComponent<Animation> ();
		anim ["Damage"].layer = 1;
		colliders = GetComponents<Collider> ();

        //slimeShooting = GetComponentInChildren<SlimeShooting>();
        currentHealth = startingHealth; // set it to a more reasonable value
    }

    void Update()
    {
        // Show health on health bar
		if (!isDead) {
			float percentage = currentHealth;
			float ratio = 0.01f;
			GetComponentInChildren<Slider> ().value = percentage * ratio;
		} else {
			if(isSinking)
				transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
			
    }


    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        if (currentHealth < 0)
            currentHealth = 0;
        //healthSlider.value = currentHealth;

		//Fade in damaged animation and disable attacking for the duration of the animation
		if (!isDead) {
			float animLength = anim ["Damage"].clip.length;
			slimeMovement.DisableAttacking ();
			anim.CrossFade ("Damage");
			Invoke ("ReenableSlimeAttacking", animLength);

		}

        slimeAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

	void ReenableSlimeAttacking(){
		slimeMovement.EnableAttacking ();
	}


    void Death()
    {
        
        isDead = true;

        game.AddConfidence(4);

        slimeAudio.clip = deathClip;
        slimeAudio.Play();

        slimeMovement.enabled = false;
        
		disableColliders ();
		disableHealthBar ();


		float animationDuration = anim ["Dead"].length;
		float waitTillRemoveTime = 2f;
		anim.CrossFade ("Dead");

		Invoke ("StartSinking", animationDuration);
		Invoke ("RemoveFromScene", animationDuration + waitTillRemoveTime);
    }

	void RemoveFromScene(){
		Destroy(gameObject);
	}

	public void StartSinking ()
	{
		isSinking = true;
		GetComponent <NavMeshAgent> ().enabled = false;
		GetComponent <Rigidbody> ().isKinematic = true; //Prevent Unity from trying to recalculate static environment
	}

    internal void TakeDamage(int damagePerShot, Vector3 point)
    {
        throw new NotImplementedException();
    }

    //
    public int currentHP()
    {
        return this.currentHealth;
    }

	private void disableColliders()
	{
		foreach (Collider c in colliders) {
			c.enabled = false;
		}
	}

	private void disableHealthBar()
	{
		gameObject.transform.FindChild ("ModelSlime").FindChild ("Canvas").gameObject.SetActive (false); 
	}
}