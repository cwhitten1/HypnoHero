using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

internal class SlimeHealth : MonoBehaviour
{
    public int startingHealth = 100000000; //Say "Hello" to my little friend!
    public int currentHealth;
    //public Slider healthSlider;
    //public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource slimeAudio;
    SlimeMovement slimeMovement;
    //SlimeShooting slimeShooting;
    bool isDead;
    bool damaged;

    void Awake()
    {
        anim = GetComponent<Animator>();
        slimeAudio = GetComponent<AudioSource>();
        slimeMovement = GetComponent<SlimeMovement>();
        //slimeShooting = GetComponentInChildren<SlimeShooting>();
        currentHealth = startingHealth;
    }

/*
    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }
*/

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

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


    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    internal void TakeDamage(int damagePerShot, Vector3 point)
    {
        throw new NotImplementedException();
    }
}