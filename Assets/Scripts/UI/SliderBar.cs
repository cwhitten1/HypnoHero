using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderBar : MonoBehaviour {

    void increase_val (string name, float val) {
        Slider slider = GameObject.Find(name).GetComponent<Slider>();
        slider.value += val;
      //  slider.value = Mathf.MoveTowards(slider.value, val, Time.deltaTime * 0.5f);

    }

    void decrease_val(string name, float val)
    {
        Slider slider = GameObject.Find(name).GetComponent<Slider>();
        if (slider.value>0)
            slider.value -= val;
        
       // slider.value = Mathf.MoveTowards(slider.value, val, Time.deltaTime * (-0.5f));
    }

    void Start()
    {
        Slider slider_M = GameObject.Find("ScareMeter").GetComponent<Slider>();
        Slider slider_C = GameObject.Find("ConfidenceMeter").GetComponent<Slider>();
        slider_M.value = 0;
        slider_C.value = 0;
        increase_val("ScareMeter", 0.4f);
       

        increase_val("ConfidenceMeter", 0.4f);
        decrease_val("ConfidenceMeter", 0.3f);
        increase_val("ConfidenceMeter", 0.2f);
    
    }
}
