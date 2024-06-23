using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public GameObject cardManager;
    private CardManager cardManagerScript;
    public GameObject enemyManager;
    private EnemyManager enemyManagerScript;
    private PlayerScript playerScript;
    public GameObject gameManager;
    private GameManager gameManagerScript;
    // Start is called before the first frame update

    private void Awake()
    {
        playerScript = PlayerScript.Instance;
    }
    void Start()
    {
        cardManagerScript = cardManager.GetComponent<CardManager>();
        enemyManagerScript = enemyManager.GetComponent<EnemyManager>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endRound()
    {
        cardManagerScript.disableCards();
        enemyManagerScript.applyDamage();
        cardManagerScript.enableCards();
        cardManagerScript.clearUsedCards();
        playerScript.setPower(playerScript.getMaxPower());
    }

    public void checkEnemies()
    {
        var victory = true;
        foreach (var enemy in enemyManagerScript.enemies)
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
            gameManagerScript.applyVictory();
        }
    }
}
