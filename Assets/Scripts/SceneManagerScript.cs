using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{

    int setSize = 5; //total enemies before entering next stage
    int currentSetScene = 0;
    public List<EnemyUIScript> enemies = new List<EnemyUIScript>();
    public List<GameObject> cards = new List<GameObject>();
    public GameObject enemyContainer;
    public GameObject player;
    private PlayerScript playerScript;
    public GameObject VictoryMenu;
    public GameObject cardCanvas;

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
            if (card.GetComponent<CardUIScript>().cardActive) card.GetComponent<CardUIScript>().DeactivateCard();
            card.gameObject.SetActive(false);
        }
        foreach (var enemy in enemies)
        {
            if (enemy.enemyAlive)
            {
                //reduce player health with the enemy's damage
                playerScript.reduceHealth(enemy.getEnemyDamage());
            }
        }
        foreach (var card in cards)
        {
            //set the cards back to active so the user can interact with them
            card.gameObject.SetActive(true);
            //clear the used cards in the carduiscripts for each player hand slot
            card.GetComponent<CardUIScript>().usedCards.Clear();
        }
        // reset player power
        playerScript.setPower();
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
            }
        }

        //prepare victory options

        if (victory)
        {
            cardCanvas.SetActive(false);
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
