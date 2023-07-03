using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    int characterHealth = 100;
    int characterMaxHealth = 100;
    int characterMana = 5;
    public Slider healthSlider;
    public Rect effectsContainer;
    private int cardLimit = 3;
    public GameObject[] playerHands;


    public GameObject sceneManager;
    SceneManagerScript sceneManagerScript;


    // Start is called before the first frame update
    void Start()
    {
        sceneManagerScript = sceneManager.GetComponent<SceneManagerScript>();
        setHand();
        setStarterCards();
        healthSlider.maxValue = characterMaxHealth;
        healthSlider.value = characterHealth;
        reduceHealth();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void setHand()
    {
        if (cardLimit <= 5 && cardLimit >=1)
        {
            foreach (var playerHand in playerHands) 
            {
                playerHand.SetActive(false);
            }

            playerHands[cardLimit - 1].SetActive(true);
            foreach (Transform child in playerHands[cardLimit - 1].transform)
            {
                if (child.gameObject)
                {
                    sceneManagerScript.cards.Add(child.gameObject);
                }
            }
        }
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
