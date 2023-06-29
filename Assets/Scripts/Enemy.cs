using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public int enemyHealth = 20;
    public int enemyDamage = 5;
    public string enemyName = "Red Cube The Destroyer";
    public bool enemyAlive = true;
    public Slider healthSlider;
    public TextMeshProUGUI enemyHealthText;
    public TextMeshProUGUI enemyNameText;
    private GameObject sceneManager;
    private SceneManagerScript sceneManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = enemyHealth;
        healthSlider.value = enemyHealth;
        sceneManager = GameObject.Find("SceneManager");
        sceneManagerScript = sceneManager.GetComponent<SceneManagerScript>();
        enemyHealthText.text = enemyHealth.ToString() + "/" + healthSlider.maxValue.ToString();
    }

    public void killEnemy()
    {
        enemyAlive = false;
        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        CardUIScript activeCard = GetActiveCard();
        Debug.Log(activeCard.cardName);
        if (activeCard != null)
        {
            enemyHealth -= activeCard.card.cardDamage;
            if (enemyHealth <= 0)
            {
                killEnemy();
            }
            else
            {
                healthSlider.value = enemyHealth;
                enemyHealthText.text = enemyHealth.ToString() + "/" + healthSlider.maxValue.ToString();
            }
        }
    }

    private CardUIScript GetActiveCard() 
    {
        foreach (var card in sceneManagerScript.cards)
        {
            CardUIScript cardScript = card.GetComponent<CardUIScript>();
            Debug.Log("loop");
            Debug.Log(cardScript.cardName);
            if (cardScript.cardActive) return cardScript;
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
