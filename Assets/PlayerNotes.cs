using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerNotes : MonoBehaviour {
	// Use this for initialization
	Bounds rdyBounds;
	bool hasNotes = true;
	void Start () {
		rdyBounds = GameObject.Find("Ready Button").GetComponent<BoxCollider2D>().bounds;
		StartCoroutine (waitPls());
	}
	IEnumerator waitPls(){

		yield return new WaitForSeconds(2.25f);

		GameObject.Find ("Pause Menu").GetComponent<PauseMenu> ().TogglePause ();
	}
	void Update(){
		if (hasNotes && Input.GetMouseButtonDown(0))
			OnMouseDown();
	}

	public void removeNotes(){
		hasNotes = false;
		GameObject.Find ("Player Notes").SetActive (false);
		GameObject.Find ("Pause Menu").GetComponent<PauseMenu> ().TogglePause ();
	}

	void OnMouseDown()
	{
		var mousePos = Input.mousePosition;
		if (rdyBounds.Contains(mousePos))
		{
			removeNotes ();
		}
	}
}
