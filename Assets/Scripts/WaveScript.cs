using UnityEngine;
using System.Collections;

public class WaveScript : MonoBehaviour {
    // arrays of tagged objects
    public GameObject[] wave_one;
    public GameObject[] wave_two;
    public GameObject[] wave_three;
    public GameObject[] wave_four;
    public GameObject[] wave_five;
    public GameObject[] wave_six;
    public GameObject boss;

    // combined hp of each wave
    public int wave_one_hp = 0;
    public int wave_two_hp = 0;
    public int wave_three_hp = 0;
    public int wave_four_hp = 0;
    public int wave_five_hp = 0;
    public int wave_six_hp = 0;

	// Use this for initialization
	void Start () {
        //
        wave_one = GameObject.FindGameObjectsWithTag("Wave1");
        wave_two = GameObject.FindGameObjectsWithTag("Wave2");
        wave_three = GameObject.FindGameObjectsWithTag("Wave3");
        wave_four = GameObject.FindGameObjectsWithTag("Wave4");
        wave_five = GameObject.FindGameObjectsWithTag("Wave5");
        wave_six = GameObject.FindGameObjectsWithTag("Wave6");
        boss = GameObject.FindGameObjectWithTag("Boss");

        //
        foreach (GameObject enemy in wave_one)
        {
            wave_one_hp += SlimeHealth.startingHealth;
            enemy.GetComponent<SlimeMovement>().canMove = true;
        }

        //
        foreach (GameObject enemy in wave_two)
        {
            wave_two_hp += SlimeHealth.startingHealth;
            enemy.GetComponent<SlimeMovement>().canMove = false;
        }
        //
        foreach (GameObject enemy in wave_three)
        {
            wave_three_hp += SlimeHealth.startingHealth;
            enemy.GetComponent<SlimeMovement>().canMove = false;
        }

        //
        foreach (GameObject enemy in wave_four)
        {
            wave_four_hp += SlimeHealth.startingHealth;
            enemy.GetComponent<SlimeMovement>().canMove = false;
        }

        //
        foreach (GameObject enemy in wave_five)
        {
            wave_five_hp += SlimeHealth.startingHealth;
            enemy.GetComponent<SlimeMovement>().canMove = false;
        }

        //
        foreach (GameObject enemy in wave_six)
        {
            wave_six_hp += SlimeHealth.startingHealth;
            enemy.GetComponent<SlimeMovement>().canMove = false;
        }
        //
        boss.GetComponent<BossMovement>().canMove = false;
    }

    // Update is called once per frame
    void Update() {
        //
        if (wave_one_hp > 0)
        {
            int n = 0;
            foreach (GameObject enemy in wave_one)
            {
                n += enemy.GetComponent<SlimeHealth>().currentHP();
            }
            wave_one_hp = n;
        }
        if (wave_one_hp <= 0)
        {
            int n = 0;
            foreach (GameObject enemy in wave_two)
            {
                enemy.GetComponent<SlimeMovement>().canMove = true;
                n += enemy.GetComponent<SlimeHealth>().currentHP();
            }
            wave_two_hp = n;
        }
        if (wave_two_hp <= 0)
        {
            int n = 0;
            foreach (GameObject enemy in wave_three)
            {
                enemy.GetComponent<SlimeMovement>().canMove = true;
                n += enemy.GetComponent<SlimeHealth>().currentHP();
            }
            wave_three_hp = n;
        }
        if (wave_three_hp <= 0) {
            int n = 0;
            foreach (GameObject enemy in wave_four)
            {
                enemy.GetComponent<SlimeMovement>().canMove = true;
                n += enemy.GetComponent<SlimeHealth>().currentHP();
            }
            wave_four_hp = n;
        }
        if (wave_four_hp <= 0) {
            int n = 0;
            foreach (GameObject enemy in wave_five)
            {
                enemy.GetComponent<SlimeMovement>().canMove = true;
                n += enemy.GetComponent<SlimeHealth>().currentHP();
            }
            wave_five_hp = n;
        }
        if (wave_five_hp <= 0) {
            int n = 0;
            foreach (GameObject enemy in wave_six)
            {
                enemy.GetComponent<SlimeMovement>().canMove = true;
                n += enemy.GetComponent<SlimeHealth>().currentHP();
            }
            wave_six_hp = n;
        }
        if (wave_six_hp <= 0)
            boss.GetComponent<BossMovement>().canMove = true;
    }
}
