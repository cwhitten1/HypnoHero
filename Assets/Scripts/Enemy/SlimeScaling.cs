using UnityEngine;
using System;
using System.Collections.Generic;

public class SlimeScaling : MonoBehaviour {
    //
    private GameObject[] wave;
    private float initial_x;
    private float initial_y;
    private float initial_z;
    private float scaleFactor;

    // Use this for initialization
    void Start () {
        //
        initial_x = 0.03f;
        initial_y = 0.03f;
        initial_z = 0.03f;
	}
	
	// Update is called once per frame
	void Update () {
        //
        wave = GameObject.FindGameObjectsWithTag("CurrentWave");

        // confidence meter max value is 100
        // scale formula 3 - (1 + (cm / 100))
        // scaleFactor will set slime to 2 * normal size at 0 confidence
        // scaleFactor will set slime to normal size at 100 confidence
        scaleFactor = (3 - (1 + ((float)Game.GetGame().GetConfidence() / 100)));
        

        //
        foreach (GameObject enemy in wave)
            enemy.transform.localScale = new Vector3(scaleFactor * initial_x, scaleFactor * initial_y, scaleFactor * initial_z);
    }
}