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
    SceneManagerScript sceneManagerScript;
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
        sceneManagerScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    //check whether card has been used and whether user has enough power to use the card
    private void OnMouseDown()
    {
        if (sceneManagerScript.usedCards.IndexOf(card) < 0)
        {
            if (playerScript.getPower() >= card.powerCost) cardActive = true;
            else Debug.Log("Not Enough Power To Use Card");
        }
        else Debug.Log("Already used card");
    }

    // check whether the card is in the usedCards array otherwise add it and deactivate the card
    public void DeactivateCard() 
    {
        if(sceneManagerScript.usedCards.IndexOf(card) < 0) sceneManagerScript.usedCards.Add(card);
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
