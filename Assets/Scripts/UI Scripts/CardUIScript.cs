using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardUIScript : MonoBehaviour
{
    //card display variable requirements
    public Card card;
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardPowerCost;
    public TextMeshProUGUI cardDamage;
    public Image cardImage;

    //card functionality variables
    CardManager cardManagerScript;
    public bool cardActive = false;

    //player relevant variables
    private PlayerScript playerScript;

    //set visible card information and get scenemanager and player scripts
    void Start()
    {
        //card display
        cardName.text = card.cardName;
        cardPowerCost.text = card.powerCost.ToString();
        cardDamage.text = card.cardDamage.ToString();
        cardImage.sprite = card.cardImage;

        //card functionality
        cardManagerScript = GameObject.Find("CardManager").GetComponent<CardManager>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    //check whether card has been used and whether user has enough power to use the card
    private void OnMouseDown()
    {
        if (cardManagerScript.usedCards.IndexOf(card) < 0)
        {
            Debug.Log(playerScript.getPower());
            Debug.Log(card.powerCost);
            if (playerScript.getPower() >= card.powerCost) cardActive = true;
            else Debug.Log("Not Enough Power To Use Card");
        }
        else Debug.Log("Already used card");
    }

    // check whether the card is in the usedCards array otherwise add it and deactivate the card
    public void DeactivateCard() 
    {
        if(cardManagerScript.usedCards.IndexOf(card) < 0) cardManagerScript.usedCards.Add(card);
        cardActive = false;
    }

    //set visible card values
    public void updateCard() 
    {
        cardName.text = card.cardName;
        cardPowerCost.text = card.powerCost.ToString();
        cardDamage.text = card.cardDamage.ToString();
        cardImage.sprite = card.cardImage;
    }

}
