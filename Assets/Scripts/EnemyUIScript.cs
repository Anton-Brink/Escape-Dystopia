using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyUIScript : MonoBehaviour
{

    //enemy variables
    public Enemy enemyAsset;
    public Enemy enemy;
    public Slider healthSlider;
    public TextMeshProUGUI enemyHealthText;
    public TextMeshProUGUI enemyNameText;
    public bool enemyAlive = true;

    //scene variables
    private GameObject sceneManager;
    private SceneManagerScript sceneManagerScript;



    //player variables
    private PlayerScript playerScript;
    void Start()
    {
        enemy = Enemy.Instantiate(enemyAsset);
        healthSlider.maxValue = enemy.enemyHealth;
        healthSlider.value = enemy.enemyHealth;
        sceneManager = GameObject.Find("SceneManager");
        sceneManagerScript = sceneManager.GetComponent<SceneManagerScript>();
        enemyHealthText.text = enemy.enemyHealth.ToString() + "/" + healthSlider.maxValue.ToString();
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    public void killEnemy()
    {
        enemyAlive = false;
        gameObject.SetActive(false);
        sceneManagerScript.checkEnemies();
    }

    private void OnMouseDown()
    {
        CardUIScript activeCard = GetActiveCard();
        if (activeCard != null)
        {
            enemy.enemyHealth -= activeCard.card.cardDamage;
            if (enemy.enemyHealth <= 0)
            {
                killEnemy();
            }
            else
            {
                healthSlider.value = enemy.enemyHealth;
                enemyHealthText.text = enemy.enemyHealth.ToString() + "/" + healthSlider.maxValue.ToString();
            }
            activeCard.DeactivateCard();
            playerScript.updatePower(activeCard.card.powerCost);
        }
    }

    private CardUIScript GetActiveCard()
    {
        foreach (var card in sceneManagerScript.cards)
        {
            CardUIScript cardScript = card.GetComponent<CardUIScript>();
            if (cardScript.cardActive) return cardScript;
        }
        return null;
    }

    public int getEnemyDamage() 
    {
        return enemy.enemyDamage;
    }
}
