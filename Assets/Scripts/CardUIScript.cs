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
    GameObject sceneManager;
    SceneManagerScript sceneManagerScript;
    public bool cardActive = false;

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
    }

    private void OnMouseDown()
    {
        Debug.Log("Click");
        deActivateOtherCards();
        cardActive = true;
    }




    private void deActivateOtherCards()
    {
        GameObject[] cards = sceneManagerScript.cards;
        foreach (GameObject card in cards)
        {
            CardUIScript cardScript = card.GetComponent<CardUIScript>();
            if (cardScript.cardActive) cardScript.cardActive = false;
        }
    }

    // Update is called once per frame
}
