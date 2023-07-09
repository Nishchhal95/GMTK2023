using System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnergyManagement : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI energyNumber;
    [SerializeField] public TextMeshProUGUI waterNumber;
    [SerializeField] public TextMeshProUGUI sunlightNumber;
    [SerializeField] public TextMeshProUGUI waterNumberPanel;
    [SerializeField] public TextMeshProUGUI sunlightNumberPanel;
    [SerializeField] public TextMeshProUGUI energyNumberPanel;
    public GameObject conversionMenuUI;
    public GameObject gameOverPanel;
    public TextMeshProUGUI timeAliveText;
    public Timer timer;

    private int amountToConsume = 1;
    private bool isOpen = false;

    public int EnergyLevel
    {
        get
        {
            return energyLevel;
        }
        set
        {
            energyLevel = value;
            energyNumber.SetText("" + energyLevel);
            energyNumberPanel.SetText("" + energyLevel);
        }
    }
    private int energyLevel = 5;
    
    public int SunLightLevel
    {
        get
        {
            return sunLightLevel;
        }
        set
        {
            sunLightLevel = value;
            sunlightNumber.SetText("" + sunLightLevel);
            sunlightNumberPanel.SetText("" + sunLightLevel);
        }
    }
    private int sunLightLevel = 5;
    
    public int WaterLevel
    {
        get
        {
            return waterLevel;
        }
        set
        {
            waterLevel = value;
            waterNumber.SetText("" + waterLevel);
            waterNumberPanel.SetText("" + waterLevel);
        }
    }
    private int waterLevel = 5;

    private void Start()
    {
        EnergyLevel = 10;
        WaterLevel = 10;
        SunLightLevel = 10;
    }

    public void ConsumeEnergy()
    {
        // Changing Energy Level Changes the UI
        EnergyLevel -= amountToConsume;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            isOpen = !isOpen;
            conversionMenuUI.SetActive(isOpen);
        }
    }

    public void ConvertToEnergy()
    {
        if (WaterLevel <= 0 || SunLightLevel <= 0)
        {
            return;
        }
        WaterLevel--;
        SunLightLevel--;
        EnergyLevel++;
    }

    public bool HaveEnergyToMove()
    {
        return energyLevel > 0;
    }

    public void AddSunlight()
    {
        SunLightLevel += 1;
    }

    public void AddWater()
    {
        WaterLevel += 1;
    }

    public void ShowGameOver()
    {
        float timeInSeconds = timer.GetTimeAlive();
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);
        string timeText = $"{timeSpan.Minutes:D2}m:{timeSpan.Seconds:D2}s";
        timeAliveText.SetText("You Survived for -> " + timeText);
        gameOverPanel.SetActive(true);
    }

    public void RestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
