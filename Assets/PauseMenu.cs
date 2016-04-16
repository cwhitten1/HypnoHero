using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

	public GUISkin myskin;

	private Canvas menu;
	private bool paused = false, waited = true;

	private void Start ()
	{
		menu = GameObject.FindGameObjectWithTag ("Pause Menu").GetComponent<Canvas> ();
		menu.enabled = false;
		StartCoroutine (PauseRoutine ());
	}

	IEnumerator PauseRoutine ()
	{
		while (true) {
				if (Input.GetKeyDown(KeyCode.P)) {
					paused = !paused;

					if (paused) Time.timeScale = 0;
					else Time.timeScale = 1;

					menu.enabled = paused;
				}
			yield return null;
		}
	}
}