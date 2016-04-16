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
	public float deathAnimSpeed = 0.75f;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	Animation anim;
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
		anim = GetComponent <Animation> ();
		anim ["Dead"].layer = 3;
		anim ["Dead"].speed = deathAnimSpeed;
		anim ["Dead"].wrapMode = WrapMode.ClampForever;

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

		anim.Play ("Dead");

		//game.RestartLevel();
		Invoke ("RestartLevel", 2);
    }

	void RestartLevel()
	{
		game.RestartLevel();
	}
}
