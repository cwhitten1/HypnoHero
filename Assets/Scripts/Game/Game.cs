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

    private int numberOfBatteries;
    private int maxBatteries;
    private float batteryLife;
    private Battery[] batteries;

    // Use this for initialization
    void Start()
    {
        InitScareAndConfidence(100, 10);
        numberOfBatteries = 3;
        maxBatteries = 3;
        batteryLife = 100;
        Battery battery1Object = GameObject.Find("Battery1").GetComponent<Battery>();
        Battery battery2Object = GameObject.Find("Battery2").GetComponent<Battery>();
        Battery battery3Object = GameObject.Find("Battery3").GetComponent<Battery>();
        batteries = new Battery[3];
        batteries[0] = battery1Object;
        batteries[1] = battery2Object;
        batteries[2] = battery3Object;
    }
	
	// Update is called once per frame
	void Update () {
	}

    public int GetBatteryLife()
    {
        return (int) Math.Ceiling(batteryLife);
    }

    public void CollectBattery()
    {
        if (numberOfBatteries < maxBatteries)
            numberOfBatteries++;
        else batteryLife = 100;
        UpdateBatteries();
    }

    public void DrainBattery(float amount)
    {
        batteryLife -= amount;
        if (batteryLife <= 0)
        {
            NextBattery();
        }
    }

    void NextBattery()
    {
        numberOfBatteries -= 1;
        batteries[numberOfBatteries].Off();
        if (numberOfBatteries > 0)
        {
            batteries[numberOfBatteries - 1].MakeCurrentBattery();
            batteryLife = 100;
        }
        UpdateBatteries();
    }

    void UpdateBatteries()
    {
        for (int i = 0; i < maxBatteries; i++)
        {
            Debug.LogWarning("BATTERY");
            if (i < numberOfBatteries)
            {
                batteries[i].On();
                batteries[i].MakeBackupBattery();
            }
            if (i == numberOfBatteries-1)
                batteries[i].MakeCurrentBattery();
            if (i >= numberOfBatteries)
                batteries[i].Off();
        }
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
        UpdateSlider(scareSlider, 100-scareMeter);
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
        UpdateSlider(scareSlider, 100-scareMeter);
    }

    /// <summary>
    /// Subtract a POSITIVE number from scare.
    /// </summary>
    public void SubtractScare(float amount)
    {
        scareMeter -= amount;
        if (scareMeter < 0)
            scareMeter = 0;
        UpdateSlider(scareSlider, 100-scareMeter);
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
