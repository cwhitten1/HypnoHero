using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {
	private GameObject startGame;
	private GameObject exitGame;
    private Bounds startBounds, endBounds;
    // Use this for initialization
    void Start () {
		startGame = GameObject.Find ("Start");
		exitGame = GameObject.Find ("Exit");
	}
	
	// Update is called once per frame
	void Update ()
    {
        startBounds = startGame.GetComponent<BoxCollider2D>().bounds;
        endBounds = exitGame.GetComponent<BoxCollider2D>().bounds;
        if (Input.GetMouseButtonDown (0))
			OnMouseDown ();
	}

	void OnMouseDown(){
		var mousePos = Input.mousePosition;
        if (startBounds.Contains(mousePos))
        {
            startGame.GetComponentInParent<AudioSource>().Play();
            SceneManager.LoadScene("level01");
        }
        else if (endBounds.Contains(mousePos))
        {
            exitGame.GetComponentInParent<AudioSource>().Play();
            Application.Quit();
        }
	}
}
