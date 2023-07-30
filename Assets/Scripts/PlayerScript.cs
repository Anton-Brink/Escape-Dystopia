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
    private int characterHealth = 100;
    private int characterMaxHealth = 100;
    public int characterPower = 0;
    private int characterMaxPower = 5;
    public Slider healthSlider;
    public Rect effectsContainer;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI playerPowerText;
    private int cardLimit = 1;
    public GameObject[] playerHands;
    public String[] playerSets = {"Techno Set","Force Set","Gadget Set"};
    private int setNumber = -1;
    private SpriteRenderer playerSpriteRenderer;
    public List<Card> technoCards = new List<Card>();
    public List<Card> forceCards = new List<Card>();
    public List<Card> gadgetCards = new List<Card>();


    public GameObject sceneManager;
    SceneManagerScript sceneManagerScript;


    // Start is called before the first frame update
    void Start()
    {
        sceneManagerScript = sceneManager.GetComponent<SceneManagerScript>();
        setHand();
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
                setCards(technoCards);
                break;
            case 1:
                playerSpriteRenderer.color = Color.cyan;
                setCards(forceCards);
                break;
            case 2:
                playerSpriteRenderer.color = Color.yellow;
                setCards(gadgetCards);
                break;


        }
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

    private void setCards(List<Card> setCards)
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
