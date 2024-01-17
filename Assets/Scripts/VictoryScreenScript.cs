using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreenScript : MonoBehaviour
{
    //Access Stuff
    public GameObject sceneManager;
    SceneManagerScript sceneManagerScript;
    PlayerScript playerScript;
    private GameObject player;
    public GameObject statGameObject;
    public GameObject componentGameObject;
    public GameObject itemGameObject;

    //UI Stuff
    public TextMeshProUGUI itemText;
    public TextMeshProUGUI componentText;
    public TextMeshProUGUI statText;
    public Image itemImage;
    public Image componentImage;
    public Image statImage;

    //Logic Stuff
    private Item randItem;
    private Stat randStat;
    private Set randSet;
    SetManager setManager;


    private GameObject selectedElement;


    // used to handle the victory screen interaction such as character selection and closing it
    public void confirmSelection()
    {
        switch (selectedElement.name)
        {
            case "Item":

                break;
            case "Component":
                //for now only do card limit, can add other effects later
                setManager.updateSetCardLimit(randSet);
                break;
            case "Stat":
                for (int i = 0; i < randStat.statArgument.Length; i++)
                {
                    switch (randStat.statType[i])
                    {
                        case "health":
                            playerScript.setHealth(playerScript.getHealth() + randStat.statArgument[i]);
                            break;
                        case "maxPower":
                            playerScript.setMaxPower(playerScript.getMaxPower() + randStat.statArgument[i]);
                            break;
                        case "maxHealth":
                            playerScript.setMaxHealth(playerScript.getMaxHealth() + randStat.statArgument[i]);
                            break;
                    }
                }
                break;
        }
        sceneManagerScript = sceneManager.GetComponent<SceneManagerScript>();
        gameObject.SetActive(false);
        sceneManagerScript.loadStartFight();
        //sceneManagerScript.transition();
    }

    public void setSelection(GameObject selectedElement) 
    {
        this.selectedElement = selectedElement;
    }

    public void prepareOptions() 
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerScript>();
        setManager = GameObject.Find("SetManager").GetComponent<SetManager>(); 
        //get random set to improve
        System.Random rand = new System.Random();
        
        randSet = setManager.getPlayerSets()[rand.Next(0, setManager.getPlayerSets().Length)];
        componentText.text = randSet.name;
        componentImage.sprite = randSet.setImage;
        componentGameObject.GetComponent<TooltipTrigger>().header = randSet.name;
        //get random item to add
        randItem = playerScript.playerItems[rand.Next(0, playerScript.playerItems.Count)];
        itemText.text = randItem.name;
        itemGameObject.GetComponent<TooltipTrigger>().header = randItem.name;
        itemGameObject.GetComponent<TooltipTrigger>().body = randItem.itemEffect;
        //get random stats to buff
        string[] scriptableObjects = AssetDatabase.FindAssets("t:Stat", new[] { "Assets/BackgroundTestSceneAssets/Sprites/Stats" });
        int statCount = scriptableObjects.Length;
        Stat[] stats = new Stat[statCount];
        for (int i = 0; i < statCount; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(scriptableObjects[i]);
            Stat stat = AssetDatabase.LoadAssetAtPath<Stat>(assetPath);
            if (stat != null)
            {
                stats[i] = stat;
            }
        }
        randStat = stats[rand.Next(0, stats.Length)];
        statText.text = randStat.statName;
        statGameObject.GetComponent<TooltipTrigger>().header = randStat.statName;
        statGameObject.GetComponent<TooltipTrigger>().body = randStat.statDescription;

        //have selection action do something and start next round
        //create set effects, items and stat changes
    }
}
