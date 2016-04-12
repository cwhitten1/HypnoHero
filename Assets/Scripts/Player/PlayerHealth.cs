using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    public bool damaged;

    Game game;


    void Awake ()
    {
        game = Game.GetGame();

        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        
        if (damaged)
        {
            //damageImage.color = flashColour;
        }
        else
        {
            //damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        game.SubtractScare(amount);

        //healthSlider.value = currentHealth*0.1f;

        //playerAudio.Play ();
        
        if (game.GetScare() <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {

        game.RestartLevel();

        isDead = true;

        //playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        //playerAudio.clip = deathClip;
        //playerAudio.Play ();

        playerMovement.enabled = false;
        //playerShooting.enabled = false;
    }
}
