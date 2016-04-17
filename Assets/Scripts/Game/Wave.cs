using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour
{
	//These two arrays match 
	public EnemyDiversityEntry[] enemyDiversity;
	private bool isWaveCleared = false;
	private bool hasWaveStarted = false;
	private string uniqueWaveTag = "CurrentWave";

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	[System.Serializable]
	public class EnemyDiversityEntry
	{
		public GameObject enemy; //Should be a prefab instance of the enemy to spawn
		public int numToSpawn; //How many should spawn in a wave
	}


	public void startWave(Transform[] spawnPoints) 
	{
		//For every type of enemy, spawn how many are supposed to be in the wave in random locations
		foreach (EnemyDiversityEntry e in enemyDiversity) 
		{
			e.enemy.tag = uniqueWaveTag; //UniqueTag is used to check if all enemies have been defeated

			for (int i = 0; i < e.numToSpawn; i++) 
			{
				int spawnPointIndex = Random.Range (0, spawnPoints.Length);
				Instantiate (e.enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
			}
		}

		hasWaveStarted = true;
	}

	public bool checkIsWaveCleared()
	{
		GameObject[] waveEnemies = GameObject.FindGameObjectsWithTag (uniqueWaveTag);

		if (waveEnemies.Length == 0) {
			Debug.LogWarning ("Wave is clear");
			return true;
		} else {
			Debug.LogWarning ("Wave is not clear: " + waveEnemies.Length + waveEnemies[0].name);
			return false;
		}
	}

	public bool getIsWaveCleared()
	{
		return isWaveCleared;
	}

	public bool getHasWaveStarted()
	{
		return hasWaveStarted;
	}
		
		
}

