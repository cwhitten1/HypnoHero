using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class avatar_change : MonoBehaviour {
    public Sprite sad;
    public Sprite happy;
    public GameObject g;
   
    // Update is called once per frame
    void Update () {
        Image image_m = GameObject.Find("Avatar").GetComponent<Image>();
        PlayerHealth health = g.GetComponent<PlayerHealth>();
       
        //Debug.Log(health.damaged);
       // image_m.sprite = sad;
        if (health.damaged == true)
        {
            //Debug.Log("Hello");
            image_m.sprite = sad;
            StartCoroutine(MyCoroutine());

        }

    }

    IEnumerator MyCoroutine()
    {
        Image image_m = GameObject.Find("Avatar").GetComponent<Image>();
        yield return new WaitForSeconds(5);
        Debug.Log("Hello waited for 5 secs");
        image_m.sprite = happy;
    }
}
