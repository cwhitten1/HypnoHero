using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class textUpdate : MonoBehaviour
{
    Text txt;
    // Use this for initialization
    float timer = 2;
    // Update is called once per frame
    void Update()
    {
        txt = gameObject.GetComponent<Text>();
        timer = timer - Time.deltaTime;
        if(timer > 0)
            txt.text = "Value now is " + timer; 
    }
}
