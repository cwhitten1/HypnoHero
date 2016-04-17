using UnityEngine;
using UnityEngine.UI;

public class FadeOverlay : MonoBehaviour {
    private Image fade;
    public float fadeSpeed = .6f;
    // Use this for initialization
    void Start () {
        fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<Image>();
        fade.GetComponentInParent<Canvas>().enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
        fade.color = Color.Lerp(fade.color, Color.clear, fadeSpeed * Time.deltaTime);
    }
}
