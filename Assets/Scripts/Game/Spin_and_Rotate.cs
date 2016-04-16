using UnityEngine;
using System.Collections;

public class Spin_and_Rotate : MonoBehaviour {

   
    Game game;
    public float amount = 20f;
    void Update()
    {
        //transform.Rotate(Vector3.up, speed * Time.deltaTime);
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    void Awake()
    {
        game = Game.GetGame();

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (game.GetScare() < 100)
            {
                game.AddScare(amount);
                Destroy(gameObject);
            }


        }
    }
}
