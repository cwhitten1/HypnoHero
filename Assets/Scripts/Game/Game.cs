using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Game : MonoBehaviour {
    private int scareMeter;
    private int confidenceMeter;

    private Slider confidenceSlider;
    private Slider scareSlider;

    // Use this for initialization
    void Start()
    {
        InitScareAndConfidence(100, 50);
    }
	
	// Update is called once per frame
	void Update () {
	}

    /// <summary>
    /// Initializes scare/confidence sliders and values.
    /// </summary>
    /// <param name="scare"></param>
    /// <param name="confidence"></param>
    void InitScareAndConfidence(int scare, int confidence)
    {
        scareMeter = scare;
        confidenceMeter = confidence;
        confidenceSlider = GameObject.Find("ConfidenceMeter").GetComponent<Slider>();
        scareSlider = GameObject.Find("ScareMeter").GetComponent<Slider>();
        UpdateSlider(confidenceSlider, confidenceMeter);
        UpdateSlider(scareSlider, scareMeter);
    }

    /// <summary>
    /// Sets a slider to the specified percentage.
    /// </summary>
    /// <param name="slider"></param>
    /// <param name="percentage"></param>
    void UpdateSlider(Slider slider, int percentage)
    {
        float ratio = 0.01f;
        slider.value = percentage * ratio;
    }

    /// <summary>
    /// Gets the confidence level. (From 0 to 100)
    /// </summary>
    public int GetConfidence()
    {
        return confidenceMeter;
    }

    /// <summary>
    ///  Will add an amount to confidence. (Max 100)
    /// </summary>
    /// <param name="amount"></param>
    public void AddConfidence(int amount)
    {
        confidenceMeter -= amount;
        UpdateSlider(confidenceSlider, confidenceMeter);
    }

    /// <summary>
    /// Gets the scare level. (From 0 to 100)
    /// </summary>
    public int GetScare()
    {
        return scareMeter;
    }

    /// <summary>
    /// Subtract a POSITIVE number from scare.
    /// </summary>
    public void SubtractScare(int amount)
    {
        scareMeter -= amount;
        UpdateSlider(scareSlider, scareMeter);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    public static Game GetGame()
    {
        return GameObject.Find("Game").GetComponent<Game>();
    }
}
