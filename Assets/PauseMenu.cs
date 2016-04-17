using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Canvas menu;
    private bool paused = false;
    
    private void Start()
    {
        menu = GameObject.FindGameObjectWithTag("Pause Menu").GetComponent<Canvas>();
        menu.enabled = false;
        StartCoroutine(PauseRoutine());
    }

    IEnumerator PauseRoutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                menu.GetComponent<AudioSource>().Play();
                paused = !paused;

                if (paused) Time.timeScale = 0;
                else Time.timeScale = 1;

                menu.enabled = paused;
            }
            yield return null;
        }
    }
}