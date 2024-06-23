using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Card> usedCards = new List<Card>();
    public List<GameObject> cards = new List<GameObject>();
    void Start()
    {
        
    }

    public void clearUsedCards() 
    {
        usedCards.Clear();
    }

    public void disableCards()
    {
        foreach (var card in cards)
        {
            //disable player cards so the user cannot click them while taking damage
            card.gameObject.SetActive(false);
        }
    }

    public void enableCards() 
    {
        foreach (var card in cards)
        {
            //set the card gameobjects back to active so the user can interact with them
            card.gameObject.SetActive(true);
            //set the carduiscript active variable to false to it does not carry over to the next round
            card.GetComponent<CardUIScript>().cardActive = false;
        }
    }
}
