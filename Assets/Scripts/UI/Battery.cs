using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Battery : MonoBehaviour {

    int batteryLife;
    Text lifeText;
    public bool currentBattery;
    Game game;

	// Use this for initialization
	void Start () {
        game = Game.GetGame();
        lifeText = gameObject.GetComponentInChildren<Text>(); ;
        batteryLife = 100;
	}
	
	// Update is called once per frame
	void Update () {
        Text txt = gameObject.GetComponentInChildren<Text>();
        if (currentBattery)
            txt.text = game.GetBatteryLife() + "%";
        else
            txt.text = "";
        //Debug.LogWarning("BATTERY");
    }

    /// <summary>
    /// This is the battery being used.
    /// </summary>
    public void MakeCurrentBattery()
    {
        On();
        currentBattery = true;
    }
    
    /// <summary>
    /// This is a battery in the inventory that hasn't been used yet
    /// because another battery is the current battery.
    /// </summary>
    public void MakeBackupBattery()
    {
        On();
        currentBattery = false;
    }

    /// <summary>
    /// This applies to batteries that are not used or partially used.
    /// </summary>
    public void On()
    {
        GetComponent<Image>().color = Color.white;
    }
    
    /// <summary>
    /// This applies to batteries that are dead and discareded.
    /// </summary>
    public void Off()
    {
        GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f);
        currentBattery = false;
    }
}
