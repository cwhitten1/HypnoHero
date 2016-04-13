using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
	public Wave[] waves;
	public Transform[] spawnPoints;
	private int currWaveIndex = 0;
	private Wave currWave;
	private float timeBetweenChecks = 5f; //Make sure we aren't checking for wave clearing every frame

	// Use this for initialization
	void Start ()
	{
		if (waves.Length > 0) 
		{
			currWave = waves [0];
			currWave.startWave (spawnPoints);
		}

		InvokeRepeating ("advanceWave", 2, timeBetweenChecks);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	private void advanceWave()
	{
		if (currWave.checkIsWaveCleared()) {
			currWaveIndex++;
			if (currWaveIndex < waves.Length) {
				currWave = waves [currWaveIndex];
				currWave.startWave (spawnPoints);
			}
		}
	}
}

