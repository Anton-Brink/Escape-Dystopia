using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : Subject
{
    public static PlayerScript Instance { get; private set; }

    //playerstats
    private int characterHealth = 0;
    private int characterMaxHealth = 100;
    private int characterPower = 0;
    private int characterMaxPower = 5;
    private int baseCharacterMaxPower = 5;

    //playerSprite
    private SpriteRenderer playerSpriteRenderer;

    //player items
    public List<Item> playerItems = new List<Item>();

    // Start is called before the first frame update
    void Awake()
    {
        
        //persistence stuff
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist across scenes


        //set/get player stats
        if (!PlayerPrefs.HasKey("maxHealth"))
        {
            setMaxHealth(characterMaxHealth);
        }
        else
        {
            setMaxHealth(PlayerPrefs.GetInt("maxHealth"));
        }
        
        if (!PlayerPrefs.HasKey("maxPower"))
        {
            setMaxPower(characterMaxPower);
        }
        else
        {
            setMaxPower(PlayerPrefs.GetInt("maxPower"));
        }
        
        if (!PlayerPrefs.HasKey("baseCharacterMaxPower"))
        {
            setBasePower(baseCharacterMaxPower);
        }
        else
        {
            setBasePower(PlayerPrefs.GetInt("baseCharacterMaxPower"));
        }


        if (!PlayerPrefs.HasKey("power"))
        {
            setPower(characterMaxPower);
        }
        else
        {
            setPower(PlayerPrefs.GetInt("power"));
        }

        if (!PlayerPrefs.HasKey("health"))
        {
            setHealth(characterMaxHealth);
        }
        else
        {
            //setHealth(PlayerPrefs.GetInt("health"));
            setHealth(characterMaxHealth);
        }

        //set player UI info
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //getters and setters below
    public void setPower(int power)
    {
        characterPower = power;
        NotifyStatObservers(power, "power");
    }

    public int getPower()
    {
        return characterPower;
    }

    public void setBasePower(int power)
    {
        baseCharacterMaxPower = power;
        NotifyStatObservers(power, "basePower");
    }

    public int getBasePower()
    {
        return baseCharacterMaxPower;
    }
    public int getMaxHealth()
    {
        return characterMaxHealth;
    }

    public void setMaxHealth(int maxHealth)
    {
        characterMaxHealth = maxHealth;
        NotifyStatObservers(maxHealth, "maxHealth");
    }
    public int getHealth()
    {
        return characterHealth;
    }
    public void setHealth(int health)
    {
        characterHealth = health;
        NotifyStatObservers(health, "health");
    }

    public int getMaxPower() 
    {
        return characterMaxPower;
    }

    public void setMaxPower(int maxPower)
    { 
        characterMaxPower = maxPower;
        NotifyStatObservers(maxPower, "maxPower");
    }

    public SpriteRenderer getPlayerSpriteRenderer()
    {
        return playerSpriteRenderer;
    }

    public void setPlayerSpriteRenderer(SpriteRenderer spriteRenderer) 
    {
        playerSpriteRenderer = spriteRenderer;
    }

    // observer stuff



}
