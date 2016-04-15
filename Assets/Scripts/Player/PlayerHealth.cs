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

    AudioSource playerAudio;
    PlayerMovement playerMovement;

    bool isDead;
    public bool damaged;

    Game game;


    void Awake ()
    {
        game = Game.GetGame();

        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
        
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
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

        isDead = true;

        playerMovement.enabled = false;

		game.RestartLevel();
    }
}
