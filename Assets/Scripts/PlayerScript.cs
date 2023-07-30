using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //playerstats
    private int characterHealth = 100;
    private int characterMaxHealth = 100;
    public int characterPower = 0;
    private int characterMaxPower = 5;

    //player stat UI
    public Slider healthSlider;
    public Rect effectsContainer;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI playerPowerText;

    //playerUI
    private SpriteRenderer playerSpriteRenderer;

    //player card management
    private int technoCardLimit = 2;
    private int forceCardLimit = 1;
    private int gadgetCardLimit = 1;
    public GameObject[] playerHands;
    public String[] playerSets = {"Techno Set","Force Set","Gadget Set"};
    private int setNumber = -1;
    public List<Card> technoCards = new List<Card>();
    public List<Card> forceCards = new List<Card>();
    public List<Card> gadgetCards = new List<Card>();

    //outside scripts
    public GameObject sceneManager;
    SceneManagerScript sceneManagerScript;


    // Start is called before the first frame update
    void Start()
    {
        //set manager script so functions can be accessed
        sceneManagerScript = sceneManager.GetComponent<SceneManagerScript>();
        //set player stats and UI info
        healthSlider.maxValue = characterMaxHealth;
        healthSlider.value = characterHealth;
        playerHealthText.text = characterHealth.ToString();
        setPower();
        playerName.text = "Twiggymocha";
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        switchPlayerSet();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setPower() 
    {
        characterPower = characterMaxPower;
        playerPowerText.text = characterPower.ToString();
    }
    public void updatePower(int PowerCost) 
    {
        characterPower-=PowerCost;
        playerPowerText.text = characterPower.ToString();
    }

    public void switchPlayerSet()
    {
        if (setNumber == 2) setNumber = 0;
        else setNumber++;
        if(setNumber < 0 || setNumber > 2) setNumber = 0;
        switch (setNumber) 
        {
            case 0:
                playerSpriteRenderer.color = Color.red;
                setHand(technoCardLimit);
                setCards(technoCards, technoCardLimit);
                break;
            case 1:
                playerSpriteRenderer.color = Color.cyan;
                setHand(forceCardLimit);
                setCards(forceCards, forceCardLimit);
                break;
            case 2:
                playerSpriteRenderer.color = Color.yellow;
                setHand(gadgetCardLimit);
                setCards(gadgetCards, gadgetCardLimit);
                break;


        }
    }

    private void setHand(int cardLimit)
    {
        if (cardLimit <= 5 && cardLimit >=1)
        {
            foreach (var playerHand in playerHands) 
            {
                playerHand.SetActive(false);
            }

            playerHands[cardLimit - 1].SetActive(true);
            sceneManagerScript.cards.Clear();
            foreach (Transform child in playerHands[cardLimit - 1].transform)
            {
                if (child.gameObject)
                {
                    sceneManagerScript.cards.Add(child.gameObject);
                }
            }
        }
    }

    private void setCards(List<Card> setCards, int cardLimit)
    {
        int currentCard = cardLimit;
        foreach (Transform child in playerHands[cardLimit - 1].transform)
        {
            currentCard--;
            CardUIScript cardScript = child.gameObject.GetComponent<CardUIScript>();
            if (cardScript != null)
            {
                cardScript.card = setCards[currentCard];
                cardScript.updateCard();
            }
        }
    }
    public void reduceHealth(int reductionAmount) 
    {
        characterHealth -= reductionAmount;
        healthSlider.value = characterHealth;
        playerHealthText.text = characterHealth.ToString();
    }
}
