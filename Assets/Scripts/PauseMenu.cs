using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameObject menu;
    private Canvas menuCanvas;
    private bool paused = false;
    private Bounds tryAgainBounds, menuBounds;
    public Color pauseBG, gameoverBG;
	public AudioClip pauseSound, gameOverSound, gameWonSound;
    bool gameOver = false, hasNotes;
	AudioSource audio;
    private void Start()
    {
        menu = GameObject.FindGameObjectWithTag("Pause Menu");
        tryAgainBounds = GameObject.Find("GOTryAgain").GetComponent<BoxCollider2D>().bounds;
        menuBounds = GameObject.Find("GOMenu").GetComponent<BoxCollider2D>().bounds;
        menuCanvas = menu.GetComponent<Canvas>();
        menuCanvas.enabled = false;
		audio = menu.GetComponent<AudioSource> ();
        StartCoroutine(PauseRoutine());
    }

    IEnumerator PauseRoutine()
    {
        while (true)
		{
            if (Input.GetMouseButtonDown(0))
                OnMouseDown();
			if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            {
				if (!gameOver)
                PauseGame();
            }
            yield return null;
        }
    }

	public void PauseGame()
    {

			GameObject.Find ("GOTitle").GetComponent<Text> ().text = "Paused";
			GameObject.Find ("GOBackground").GetComponent<Image> ().color = pauseBG;
			GameObject.Find ("GOTryAgain").GetComponent<Text> ().enabled = false;
			GameObject.Find ("GOMenu").GetComponent<Text> ().enabled = false;
			audio.clip = pauseSound;
			menu.GetComponent<AudioSource> ().Play ();     

        menuCanvas.enabled = !paused;
		TogglePause ();
    }

	public void TogglePause(){
		paused = !paused;

		if (paused)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}
    public void GameOver()
	{
		audio.clip = gameOverSound;
		menu.GetComponent<AudioSource> ().Play ();    
		gameOver = true;
        menuCanvas.enabled = true;
        menu.GetComponentInChildren<Image>().color = gameoverBG;
        GameObject.Find("GOTitle").GetComponent<Text>().text = "Game Over";
        GameObject.Find("GOTryAgain").GetComponent<Text>().enabled = true;
        GameObject.Find("GOMenu").GetComponent<Text>().enabled = true;
    }

	public void GameWon(){
		audio.clip = gameWonSound;
		//menu.GetComponent<AudioSource> ().Play ();    
		gameOver = true;
		menuCanvas.enabled = true;
		menu.GetComponentInChildren<Image>().color = gameoverBG;
		GameObject.Find("GOTitle").GetComponent<Text>().text = "Nightmare No More!";
		GameObject.Find("GOTryAgain").GetComponent<Text>().enabled = true;
		GameObject.Find("GOMenu").GetComponent<Text>().enabled = true;
	}

    void OnMouseDown()
    {
        if (!gameOver) return;
        var mousePos = Input.mousePosition;
        if (tryAgainBounds.Contains(mousePos))
        {
            Game.GetGame().RestartLevel();
        }
        else if (menuBounds.Contains(mousePos))
        {
            SceneManager.LoadScene("start");
        }
    }
}