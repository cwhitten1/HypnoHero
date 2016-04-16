using UnityEngine;
using System;
using System.Collections.Generic;

public class SlimeScaling : MonoBehaviour
{
    //
    private GameObject[] wave;
    private const float initial_scale = 0.03f;
    public float scaleFactor = 2f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //
        const int maxConfidence = 100;
        float currConfidence = (float)Game.GetGame().GetConfidence();
        wave = GameObject.FindGameObjectsWithTag("CurrentWave");

        // confidence meter max value is 100
        // scale formula 3 - (1 + (cm / 100))
        // scaleFactor will set slime to 2 * normal size at 0 confidence
        // scaleFactor will set slime to normal size at 100 confidence
        scaleFactor = (3 - (1 + (currConfidence / maxConfidence)));

        //
        foreach (GameObject enemy in wave)
            enemy.transform.localScale = new Vector3(
                scaleFactor * initial_scale,
                scaleFactor * initial_scale,
                scaleFactor * initial_scale
            );
    }
}