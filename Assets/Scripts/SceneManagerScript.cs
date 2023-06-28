using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{

    int setSize = 5; //total enemies before entering next stage
    int currentSetScene = 0;
    public GameObject[] cards;
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        if (currentSetScene == 0)
        {
            loadStartFight();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void loadStartFight() 
    {
        spawnCombatInstance();
        getPlayerCards();
    }

    void spawnCombatInstance()
    {
        
    }

    void getPlayerCards()
    {

    }
}
