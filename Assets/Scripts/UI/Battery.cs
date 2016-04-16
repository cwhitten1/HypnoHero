using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Battery : MonoBehaviour {

    int batteryLife;
    Text lifeText;

	// Use this for initialization
	void Start () {
        lifeText = gameObject.GetComponentInChildren<Text>(); ;
        batteryLife = 100;
	}
	
	// Update is called once per frame
	void Update () {
        Text txt = gameObject.GetComponentInChildren<Text>();
        if (batteryLife > 0)
            txt.text = batteryLife + "%";
    }

    public void BlackOut()
    {
        GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f);
    }
}
