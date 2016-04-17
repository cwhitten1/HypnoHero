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
    bool gameOver = false;
    private void Start()
    {
        menu = GameObject.FindGameObjectWithTag("Pause Menu");
        tryAgainBounds = GameObject.Find("GOTryAgain").GetComponent<BoxCollider2D>().bounds;
        menuBounds = GameObject.Find("GOMenu").GetComponent<BoxCollider2D>().bounds;
        menuCanvas = menu.GetComponent<Canvas>();
        menuCanvas.enabled = false;
        StartCoroutine(PauseRoutine());
    }

    IEnumerator PauseRoutine()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
                OnMouseDown();
            if (Input.GetKeyDown(KeyCode.P))
            {
                PauseGame();
            }
            yield return null;
        }
    }

    public void PauseGame()
    {
        GameObject.Find("GOTitle").GetComponent<Text>().text = "Paused";
        GameObject.Find("GOBackground").GetComponent<Image>().color = pauseBG;
        GameObject.Find("GOTryAgain").GetComponent<Text>().enabled = false;
        GameObject.Find("GOMenu").GetComponent<Text>().enabled = false;
        menu.GetComponent<AudioSource>().Play();

        paused = !paused;

        if (paused) Time.timeScale = 0;
        else Time.timeScale = 1;

        menuCanvas.enabled = paused;
    }

    public void GameOver()
    {
        menuCanvas.enabled = true;
        menu.GetComponentInChildren<Image>().color = gameoverBG;
        GameObject.Find("GOTitle").GetComponent<Text>().text = "Game Over";
        GameObject.Find("GOTryAgain").GetComponent<Text>().enabled = true;
        GameObject.Find("GOMenu").GetComponent<Text>().enabled = true;
    }

    void OnMouseDown()
    {

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