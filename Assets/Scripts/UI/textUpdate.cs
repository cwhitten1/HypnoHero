using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class textUpdate : MonoBehaviour
{
    Text txt;
    // Use this for initialization
  
    // Update is called once per frame
    void Update()
    {
        txt = gameObject.GetComponent<Text>();
        Slider slider_M = GameObject.Find("ScareMeter").GetComponent<Slider>();
        Slider slider_C = GameObject.Find("ConfidenceMeter").GetComponent<Slider>();
        double scare_meter = System.Math.Round(slider_M.value, 1) * 100;
        double confidence_meter = System.Math.Round(slider_C.value, 1) * 100;
        txt.text = "Scare Meter: " + scare_meter+"%" + "\n\n Confidence Meter: " + confidence_meter+"%"; 
    }
}
