using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class Game : MonoBehaviour {
    private float scareMeter;
    private float confidenceMeter;

    private Slider confidenceSlider;
    private Slider scareSlider;

    // Use this for initialization
    void Start()
    {
        InitScareAndConfidence(100, 0);
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
    void UpdateSlider(Slider slider, float percentage)
    {
        float ratio = 0.01f;
        slider.value = percentage * ratio;
    }

    /// <summary>
    /// Gets the confidence level. (From 0 to 100)
    /// </summary>
    public int GetConfidence()
    {
        return (int) System.Math.Ceiling(confidenceMeter);
    }

    /// <summary>
    ///  Will add an amount to confidence. (Max 100)
    /// </summary>
    /// <param name="amount"></param>
    public void AddConfidence(float amount)
    {
        confidenceMeter += amount;
        if (confidenceMeter > 100)
            confidenceMeter = 100;
        UpdateSlider(confidenceSlider, confidenceMeter);
    }
    
    /// <summary>
    /// Subtract a POSITIVE number from confidence.
    /// </summary>
    /// <param name="amount"></param>
    public void SubtractConfidence(float amount)
    {
        confidenceMeter -= amount;
        if (confidenceMeter < 0)
            confidenceMeter = 0;
        UpdateSlider(confidenceSlider, confidenceMeter);
    }

    /// <summary>
    /// Gets the scare level. (From 0 to 100)
    /// </summary>
    public int GetScare()
    {
        return (int) System.Math.Ceiling(scareMeter);
    }

    public void AddScare(float amount)
    {
        scareMeter += amount;
        if (scareMeter > 100)
            scareMeter = 100;
        UpdateSlider(scareSlider, scareMeter);
    }

    /// <summary>
    /// Subtract a POSITIVE number from scare.
    /// </summary>
    public void SubtractScare(float amount)
    {
        scareMeter -= amount;
        if (scareMeter < 0)
            scareMeter = 0;
        UpdateSlider(scareSlider, scareMeter);
    }

   

    public void RestartLevel()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene ().buildIndex);
    }

    public static Game GetGame()
    {
        return GameObject.Find("Game").GetComponent<Game>();
    }
}
