using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{

    int setSize = 5; //total enemies before entering next stage
    int currentSetScene = 0;
    public List<Enemy> enemies = new List<Enemy>();
    public List<GameObject> cards = new List<GameObject>();
    public GameObject enemyContainer;
    public GameObject player;
    private PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {   
        playerScript = player.GetComponent<PlayerScript>();
        if (currentSetScene == 0)
        {
            loadStartFight();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endRound() 
    {
        foreach (var card in cards) 
        {
            card.gameObject.SetActive(false);
        }
        foreach (var enemy in enemies)
        {
            playerScript.healthSlider.value -= enemy.enemyDamage;
        }
        foreach (var card in cards)
        {
            card.gameObject.SetActive(true);
        }

    }

    void loadStartFight() 
    {
        spawnCombatInstance();
        getPlayerCards();
    }

    void spawnCombatInstance()
    {
        foreach (Transform childContainer in enemyContainer.transform)
        {
            if (childContainer)
            {
                foreach (Transform enemyChild in childContainer.transform)
                {

                    Enemy enemy = enemyChild.GetComponent<Enemy>();
                    enemies.Add(enemy);
                }
            }
        }
        foreach (var enemy in enemies)
        {
            Debug.Log(enemy.enemyDamage);
        }
    }

    void getPlayerCards()
    {

    }
}
