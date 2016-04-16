using UnityEngine;
using System.Collections;

public class MediKit_Spawn : MonoBehaviour {
    public GameObject medikit;
    public Transform[] spawnPoints;
    public float time_to_spawn = 20;
    Game game;
    // Use this for initialization

    void Start () {
        InvokeRepeating("startWave", 0, time_to_spawn);
    }
    void Awake()
    {
        game = Game.GetGame();
        
    }
	
	// Update is called once per frame
	void Update () {
        
         //   startWave(spawnPoints);

    }
    public void startWave()
    {
        if (game.GetScare() < 50 && checkIsWaveCleared() == true)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(medikit, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
    }

    public bool checkIsWaveCleared()
    {
        GameObject[] medikit = GameObject.FindGameObjectsWithTag("MediKit");

        if (medikit.Length == 0)
        {
            Debug.LogWarning ("Wave is clear");
            return true;
        }
        else {
            Debug.LogWarning ("Wave is not clear: " + medikit.Length + medikit[0].name);
            return false;
        }
    }
}
