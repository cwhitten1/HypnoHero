using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class textUpdate : MonoBehaviour
{
    Game game;

    Text txt;
    // Use this for initialization
  
    // Update is called once per frame
    void Update()
    {
        game = Game.GetGame();
        txt = gameObject.GetComponent<Text>();
        int scare_meter = game.GetScare();
        int confidence_meter = game.GetConfidence();
        txt.text = "Scare Meter: " + scare_meter+"%" + "\n\n Confidence Meter: " + confidence_meter+"%"; 
    }
}
