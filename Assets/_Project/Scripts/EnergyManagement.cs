using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
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
    private int waterLevel = 10;
    private int sunlightLevel = 10;
    private int totalEnergyLevel = 5;
    private bool isOpen = false;
    

    public int CostEnergy(int energyLevel)
    {
        if(totalEnergyLevel == 0)
        {
            return energyLevel = 0;
        }
        else
        {
            energyLevel = totalEnergyLevel;
            energyLevel -= 1;
            energyNumber.text = energyLevel.ToString();
            energyNumberPanel.text = energyLevel.ToString();
            totalEnergyLevel = energyLevel;
            return energyLevel;
        }
        
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
        if(waterLevel > 0 || sunlightLevel > 0)
        {
            waterLevel -= 1;
            sunlightLevel -= 1;
            totalEnergyLevel += 1;
            waterNumber.text = waterLevel.ToString();
            waterNumberPanel.text = waterLevel.ToString();
            sunlightNumber.text = sunlightLevel.ToString();
            sunlightNumberPanel.text = sunlightLevel.ToString();
            energyNumber.text = totalEnergyLevel.ToString();
            energyNumberPanel.text = totalEnergyLevel.ToString();
        }
        else
        {
            return;
        }
    }

}
