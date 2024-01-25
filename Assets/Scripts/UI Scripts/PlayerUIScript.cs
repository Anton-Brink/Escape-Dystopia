using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIScript : MonoBehaviour, StatObserver
{
    public Slider healthSlider;
    public Rect effectsContainer;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI playerPowerText;
    public TextMeshProUGUI playerMaxHealthText; // this should include a slash and space
    [SerializeField] Subject subject;

    private void Start()
    {
        PlayerScript playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        healthSlider.maxValue = playerScript.getMaxHealth();
        playerMaxHealthText.text = " / " + healthSlider.maxValue.ToString();
        healthSlider.value = playerScript.getHealth();
        playerHealthText.text = playerScript.getHealth().ToString();
        playerName.text = "Twiggymocha";
    }

    public void statChange(int changeAmount, string statName)
    {
        switch (statName)
        {
            case "health":
                healthSlider.value = changeAmount;
                playerHealthText.text = changeAmount.ToString();
                break;
            case "maxHealth":
                healthSlider.maxValue = changeAmount;
                playerMaxHealthText.text = " / " + changeAmount.ToString();
                break;
            case "power":
                playerPowerText.text = changeAmount.ToString();
                break;
        }
    }

    private void OnEnable()
    {
        subject.AddStatObserver(this);
    }

    private void OnDisable()
    {
        subject.RemoveStatObserver(this);
    }
}
