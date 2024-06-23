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
    [SerializeField] private Subject subject;

    private void Awake()
    {
        if (PlayerScript.Instance == null)
        {
            Debug.LogError("PlayerScript instance is not initialized. Make sure PlayerScript is in the scene and initialized.");
            return;
        }
        subject = PlayerScript.Instance;
    }
    private void Start()
    {
        setPlayerStats();
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
        if (subject != null)
        {
            Debug.Log("Stat Observer Added");
            subject.AddStatObserver(this);
        }
    }

    private void OnDisable()
    {
        if (subject != null)
        {
            subject.RemoveStatObserver(this);
        }

    }
    private void setPlayerStats()
    {
        if (PlayerPrefs.HasKey("maxHealth"))
        {
            healthSlider.maxValue = PlayerPrefs.GetInt("maxHealth"); ;
            playerMaxHealthText.text = " / " + healthSlider.maxValue.ToString();
        }
        else
        {
            healthSlider.maxValue = 100;
            playerMaxHealthText.text = " / " + healthSlider.maxValue.ToString();
        }

        if (PlayerPrefs.HasKey("health"))
        {
            healthSlider.value = PlayerPrefs.GetInt("health");
            playerHealthText.text = PlayerPrefs.GetInt("health").ToString();
        }
        else
        {
            healthSlider.value = 100;
            playerHealthText.text = healthSlider.value.ToString();
        }

        if (PlayerPrefs.HasKey("playerName")) healthSlider.value = PlayerPrefs.GetInt("health");
        else playerName.text = "Twiggymocha";
    }
}
