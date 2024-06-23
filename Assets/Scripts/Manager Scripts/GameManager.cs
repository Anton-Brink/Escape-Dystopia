using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyManager;
    private EnemyManager enemyManagerScript;
    
    public GameObject cardCanvas;
    
    private PlayerScript playerScript;
    
    public GameObject itemManager;
    private ItemManager itemManagerScript;

    public GameObject cardManager;
    private CardManager cardManagerScript;

    public GameObject VictoryMenu;
    public RoundData roundData;

    private void Awake()
    {
        playerScript = PlayerScript.Instance;
    }

    void Start()
    {
        var enemyType = roundData.path[roundData.pathRound];
        if (enemyType != "Normal" && enemyType != "Elite" && enemyType != "Boss")
        {
            prepareEnemies("Normal");
        }
        else
        {
            prepareEnemies(enemyType);
        }
        prepareCards();
        prepareBasePlayerStats();
        applyItems();
        clearCards();
        setModifiedPlayerStats();
    }

    private void prepareEnemies(string enemyType) 
    {
        enemyManagerScript = enemyManager.GetComponent<EnemyManager>();
        enemyManagerScript.spawnCombatInstance(enemyType);
    }
    private void prepareCards()
    {
        cardCanvas.SetActive(true);
    }
    private void prepareBasePlayerStats() 
    {
        playerScript.setMaxPower(playerScript.getBasePower());
    }
    private void applyItems() 
    {
        itemManagerScript = itemManager.GetComponent<ItemManager>();
        itemManagerScript.applyPerTurnItemEffects();
    }
    private void clearCards() 
    {
        cardManagerScript = cardManager.GetComponent<CardManager>();
        cardManagerScript.clearUsedCards();
        cardManagerScript.usedCards.Clear();
    }
    private void setModifiedPlayerStats()
    {
        playerScript.setPower(playerScript.getMaxPower());
    }

    public void applyVictory() 
    {
        cardCanvas.SetActive(false);
        playerScript.setPower(playerScript.getMaxPower());
        var victoryScript = VictoryMenu.GetComponent<VictoryScreenScript>();
        victoryScript.prepareOptions();
        VictoryMenu.SetActive(true);
    }
}
