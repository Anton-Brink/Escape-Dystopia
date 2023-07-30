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
    public List<Card> usedCards = new List<Card>();

    //card functionality variables
    GameObject sceneManager;
    SceneManagerScript sceneManagerScript;
    public bool cardActive = false;

    //player relevant variables
    private PlayerScript playerScript;

    void Start()
    {
        //card display
        cardName.text = card.cardName;
        cardPowerCost.text = card.powerCost.ToString();
        cardDamage.text = card.cardDamage.ToString();
        cardImage.sprite = card.cardImage;
        
        //card functionality
        sceneManager = GameObject.Find("SceneManager");
        sceneManagerScript = sceneManager.GetComponent<SceneManagerScript>();

        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    private void OnMouseDown()
    {
        if (usedCards.IndexOf(card) < 0)
        {
            if (playerScript.characterPower >= card.powerCost) cardActive = true;
            else Debug.Log("Not Enough Power To Use Card");
        }
        else Debug.Log("Already used card");
    }

    public void DeactivateCard() 
    {
        Debug.Log(card);
        if(usedCards.IndexOf(card) < 0) usedCards.Add(card);
        cardActive = false;
    }

    public void updateCard() 
    {
        cardName.text = card.cardName;
        cardPowerCost.text = card.powerCost.ToString();
        cardDamage.text = card.cardDamage.ToString();
        cardImage.sprite = card.cardImage;
    }


    private void deActivateOtherCards()
    {
        foreach (GameObject card in sceneManagerScript.cards)
        {
            CardUIScript cardScript = card.GetComponent<CardUIScript>();
            if (cardScript.cardActive) cardScript.cardActive = false;
        }
    }

    // Update is called once per frame
}
