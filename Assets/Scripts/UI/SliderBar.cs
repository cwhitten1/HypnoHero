using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderBar : MonoBehaviour {

    public void increase_val (string name, float val) {
        Slider slider = GameObject.Find(name).GetComponent<Slider>();
        slider.value += val;
      //  slider.value = Mathf.MoveTowards(slider.value, val, Time.deltaTime * 0.5f);

    }

    public void decrease_val(string name, float val)
    {
        Slider slider = GameObject.Find(name).GetComponent<Slider>();
        if (slider.value>0)
            slider.value -= val;
        
       // slider.value = Mathf.MoveTowards(slider.value, val, Time.deltaTime * (-0.5f));
    }

    public void set_val(string name, float val)
    {
        Slider slider = GameObject.Find(name).GetComponent<Slider>();
        slider.value = val;
    }

    void Start()
    {
    }
}
