using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderBar : MonoBehaviour {

	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
        Slider slider = GetComponent<Slider>();
        slider.value = Mathf.MoveTowards(slider.value, 1f, Time.deltaTime * 0.5f);

    }
}
