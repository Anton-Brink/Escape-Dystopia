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
    //playerstats
    private int characterHealth = 100;
    private int characterMaxHealth = 100;
    private int characterPower = 0;
    private int characterMaxPower = 5;

    //playerSprite
    private SpriteRenderer playerSpriteRenderer;

    //player items
    public List<Item> playerItems = new List<Item>();

    // Start is called before the first frame update
    void Awake()
    {
        //set player stats and UI info
        setPower(characterMaxPower);
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
