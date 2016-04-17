using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {
	private GameObject startGame;
	private GameObject exitGame;
	// Use this for initialization
	void Start () {
		startGame = GameObject.Find ("Start");
		exitGame = GameObject.Find ("Exit");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0))
			OnMouseDown ();
	}

	void OnMouseDown(){;
		var mousePos = Input.mousePosition;
		Bounds startBounds = startGame.GetComponent<BoxCollider2D> ().bounds;
		Bounds endBounds = exitGame.GetComponent<BoxCollider2D> ().bounds;
		if (startBounds.Contains (mousePos))
			SceneManager.LoadScene ("level01");
		if (endBounds.Contains (mousePos))
			Application.Quit ();

	}
}
