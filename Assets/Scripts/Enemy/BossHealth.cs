using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

internal class BossHealth : MonoBehaviour
{
    public static int startingHealth = 10000; //Say "Hello" to my little friend!
    public int currentHealth;
    //public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource slimeAudio;
    BossMovement slimeMovement;
    //SlimeShooting slimeShooting;
    bool isDead;
    bool damaged;

    Game game;

    void Awake()
    {
        game = Game.GetGame();

        anim = GetComponent<Animator>();
        slimeAudio = GetComponent<AudioSource>();
        slimeMovement = GetComponent<BossMovement>();
        //slimeShooting = GetComponentInChildren<SlimeShooting>();
        currentHealth = startingHealth; // set it to a more reasonable value
    }

    void Update()
    {
        if (damaged)
        {
            // damageImage.color = flashColour;
        }
        else
        {
            // damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        if (currentHealth < 0)
            currentHealth = 0;
        //healthSlider.value = currentHealth;

        slimeAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {

        isDead = true;

        //slimeShooting.DisableEffects();

        anim.SetTrigger("Die");

        slimeAudio.clip = deathClip;
        slimeAudio.Play();

        slimeMovement.enabled = false;
        //slimeShooting.enabled = false;
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
}