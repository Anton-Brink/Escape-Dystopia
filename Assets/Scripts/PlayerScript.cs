using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    int characterHealth = 100;
    int characterMaxHealth = 100;
    int characterMana = 5;
    public Slider healthSlider;
    public Rect effectsContainer;
    public SceneManagerScript sceneManagerScript;
    public Card[] cards;
    
    
    // Start is called before the first frame update
    void Start()
    {
        setStarterCards();
        healthSlider.maxValue = characterMaxHealth;
        healthSlider.value = characterHealth;
        sceneManagerScript = GetComponent<SceneManagerScript>();
        reduceHealth();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void setStarterCards()
    {
        for (int i = 0; i < 4; i++)
        {
            
        }
    }
    void reduceHealth() 
    {
        characterHealth -= 10;
        healthSlider.value = characterHealth;
    }
}
