using UnityEngine;
using System.Collections;

public class StealthObjects : MonoBehaviour {
	public bool isStealth = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.name == "Player") {
			isStealth = true;
		}
	}
	void OnTriggerExit(Collider other){
		if (other.name == "Player") {
			isStealth = false;
		}
	}
}
