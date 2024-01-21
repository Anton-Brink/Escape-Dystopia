using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetManager : MonoBehaviour
{
    //player card management
    public GameObject[] playerHands;
    public Set[] playerSets;
    private int setNumber = -1;
    private SceneManagerScript sceneManagerScript;
    private int previousCardLimit = 0; //this keeps track of the previous card limit so we know which hand to disable

    // Start is called before the first frame update
    void Start()
    {
        sceneManagerScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
        //set cardArrays
        switchPlayerSet();
    }


    public void switchPlayerSet()
    {
        Debug.Log("setNumber: " + setNumber);
        if(setNumber < 0 || setNumber > 2) previousCardLimit = 0;//if set number is out of the valid range set the previous card limit to 0
        else previousCardLimit = playerSets[setNumber].cardLimit - 1;

        if (setNumber == 2) setNumber = 0;
        else setNumber++;
        if (setNumber < 0 || setNumber > 2) setNumber = 0;//if set number is out of the valid range set the setNumber to 0

        setHand(playerSets[setNumber].cardLimit);
        
        PlayerScript playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        SpriteRenderer playerSprite = playerScript.getPlayerSpriteRenderer();

        playerSprite.color = playerSets[setNumber].components;//make this a function to equip multiple componets (armor pieces)
        playerScript.setPlayerSpriteRenderer(playerSprite);
    }

    private void setHand(int cardLimit)
    {

        //make sure set has valid cardLimit set
        if (cardLimit <= 5 && cardLimit >= 1)
        {
            //set the previous active player hand to inactive
            if (previousCardLimit != -1)
            {
                playerHands[previousCardLimit].SetActive(false);
            }
            else
            {
                //set all hands as inactive if there was a problem getting the previous set's card limit
                foreach (var playerHand in playerHands)
                {
                    playerHand.SetActive(false);
                }
            }
            //set the active hand to the current set's card limit
            playerHands[cardLimit - 1].SetActive(true);

            sceneManagerScript.cards.Clear();
            foreach (Transform child in playerHands[cardLimit - 1].transform)
            {
                if (child.gameObject)
                {
                    sceneManagerScript.cards.Add(child.gameObject);
                }
            }
            //set the hexagon card ui script values
            setUsableCards(playerSets[setNumber].setCards, playerSets[setNumber].cardLimit);
        }
    }

    private void setUsableCards(Card[] setCards, int cardLimit)
    {
        int currentCard = cardLimit;
        //loop through the available card spots for the hand
        foreach (Transform child in playerHands[cardLimit - 1].transform)
        {
            currentCard--;
            //get the ui script object for the available card spot in the hand
            CardUIScript cardScript = child.gameObject.GetComponent<CardUIScript>();
            if (cardScript != null)
            {
                //set the card variable in the card script
                cardScript.card = setCards[currentCard];
                //update the visible card information
                cardScript.updateCard();
            }
        }
    }

    public Set[] getPlayerSets() 
    {
        return playerSets;
    }

    public void updateSetCardLimit(Set updateSet) 
    {
        updateSet.cardLimit++;
    }



}
