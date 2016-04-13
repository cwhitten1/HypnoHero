using UnityEngine;
using System;
using System.Collections.Generic;

public class SlimeScaling : MonoBehaviour {
    //
    private GameObject[] waves;
    private List<Vector3> scales;
    private List<GameObject[]> slimes;
    private Game gameScript;
    private List<string> tags;

    // Use this for initialization
    void Start () {
        //
        gameScript = Game.GetGame();
        scales = new List<Vector3>();
        slimes = new List<GameObject[]>();
        tags = new List<string>();
        
        // add relevant tags to tags[]
        tags.Add("Wave1");
        tags.Add("Wave2");
        tags.Add("Wave3");
        tags.Add("Wave4");
        tags.Add("Wave5");
        tags.Add("Wave6");

        // add relevant tags to slimes[]
        foreach (string element in tags)
        {
            waves = GameObject.FindGameObjectsWithTag(element);
            slimes.Add(waves);
        }

        //
        Debug.Log(slimes[0].Length);
        Debug.Log(slimes[1].Length);
        Debug.Log(slimes[2].Length);
        Debug.Log(slimes[3].Length);
        Debug.Log(slimes[4].Length);
        Debug.Log(slimes[5].Length);


        // add scales to scales[]
        foreach (GameObject[] wave in slimes)
        {
            foreach (GameObject enemy in waves)
            {
                scales.Add(enemy.transform.localScale);
            }
        }

        foreach (Vector3 scale in scales)
            Debug.Log(scale);
	}
	
	// Update is called once per frame
	void Update () {
        // confidence meter max value is 100
        // scale formula 3 - (1 + (cm / 100))
        float scaleFactor = 3 - (1 + (gameScript.GetConfidence() / 100));

        //
        int i = 0;
        foreach (GameObject[] waves in slimes)
        {
            foreach (GameObject enemy in waves)
            {
                enemy.transform.localScale = new Vector3(scaleFactor * scales[i].x, scaleFactor * scales[i].y, scaleFactor * scales[i].z);
                i++;
            }
        }
    }
}
