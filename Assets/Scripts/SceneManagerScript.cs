using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{

    int enemyStageSize = 5; //total enemies before entering next stage
    int currentSetScene = 0;
    public List<EnemyUIScript> enemies = new List<EnemyUIScript>();
    public List<GameObject> cards = new List<GameObject>();
    public GameObject enemyContainer;
    public GameObject player;
    private PlayerScript playerScript;
    public GameObject VictoryMenu;
    public GameObject cardCanvas;
    public List<Card> usedCards = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {   
        playerScript = player.GetComponent<PlayerScript>();
        if (currentSetScene == 0)
        {
            loadStartFight();
        }
    }

    public void endRound() 
    {
        foreach (var card in cards) 
        {
            //disable player cards so the user cannot click them while taking damage
            card.gameObject.SetActive(false);
        }
        foreach (var enemy in enemies)
        {
            if (enemy.enemyAlive)
            {
                //reduce player health with the enemy's damage
                int currentPlayerHealth = playerScript.getHealth();
                currentPlayerHealth -= enemy.getEnemyDamage();
                playerScript.setHealth(currentPlayerHealth);
                Debug.Log("enter here");
            }
        }
        foreach (var card in cards)
        {
            //set the card gameobjects back to active so the user can interact with them
            card.gameObject.SetActive(true);
            //set the carduiscript active variable to false to it does not carry over to the next round
            card.GetComponent<CardUIScript>().cardActive = false;
        }
        //clear the used cards
        usedCards.Clear();
        // reset player power
        playerScript.setPower(playerScript.getMaxPower());
    }

    //check whether any enemies are remaining
    public void checkEnemies() 
    {
        var victory = true;
        foreach (var enemy in enemies)
        {
            if (enemy.enemyAlive)
            {
                // if an enemy is alive victory is false
                victory = false;
                break;
            }
        }
        //prepare victory options
        if (victory)
        {
            cardCanvas.SetActive(false);
            var victoryScript = VictoryMenu.GetComponent<VictoryScreenScript>();
            victoryScript.prepareOptions();
            VictoryMenu.SetActive(true);
        }
    }

    public void loadStartFight() 
    {
        cardCanvas.SetActive(true);
        spawnCombatInstance();
    }

    void spawnCombatInstance()
    {
        //loop through the enemy instances in the enemy container
        foreach (Transform childContainer in enemyContainer.transform)
        {
            if (childContainer)
            {
                //loop through the enemies in an enemy instance
                foreach (Transform enemyChild in childContainer.transform)
                {
                    if (!enemyChild.gameObject.activeSelf) enemyChild.gameObject.SetActive(true);
                    EnemyUIScript enemy = enemyChild.GetComponent<EnemyUIScript>();
                    enemies.Add(enemy);
                }
            }
        }
    }
}
