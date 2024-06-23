using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class RestManager : MonoBehaviour
{
    public RoundData RoundData;
    private SceneManagerScript sceneManagerScript;
    private int increaseAmount = 0;
    public GameObject sceneManager;

    void Awake() 
    {
        increaseAmount = calculateRestHealthReturn();
    
    }

    public void loadRound()
    {
        sceneManagerScript = sceneManager.GetComponent<SceneManagerScript>();
        if (RoundData.pathRound > RoundData.path.Length)
        {
            RoundData.pathRound = 0;
        }
        else
        {
            RoundData.pathRound++;
        }
        string nextScene = RoundData.path[RoundData.pathRound];
        switch (nextScene)
        {
            case "Normal":
            case "Elite":
            case "Boss":
                sceneManagerScript.loadScene("Combat");
                break;
            case "Rest":
                sceneManagerScript.loadScene("Rest");
                break;
            default:
                sceneManagerScript.loadScene("Combat");
                break;
        }
    }
    public void healPlayer() 
    {
        try
        {
            Debug.Log(PlayerScript.Instance.getHealth());
            var newHealth = PlayerScript.Instance.getHealth() + increaseAmount;
            Debug.Log(newHealth);
            PlayerScript.Instance.setHealth(newHealth);
            loadRound();
        }
        catch 
        {
            Debug.Log("Could Not Update Player Health");
        }
    }

    private int calculateRestHealthReturn() 
    {
        int returnVal = 0;
        int currentHealth = 1;
        int maxHealth = 100;
        try
        {
            maxHealth = PlayerScript.Instance.getMaxHealth();
            currentHealth = PlayerScript.Instance.getHealth();

            returnVal = (int)(maxHealth * 0.2);

            if (currentHealth + returnVal > maxHealth)
            {
                returnVal = maxHealth - currentHealth;
            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
            Debug.Log("Could Not Calculate Rest Health Return Value");
            returnVal = 0;
        }

        return returnVal;
    }

    public void scavenge()
    {
        System.Random rand = new System.Random();
        string[] scriptableItemObjects = AssetDatabase.FindAssets("t:Item", new[] { "Assets/BackgroundTestSceneAssets/Sprites/Items" });
        int itemCount = scriptableItemObjects.Length;
        Debug.Log(itemCount);
        Item[] items = new Item[itemCount];
        for (int i = 0; i < itemCount; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(scriptableItemObjects[i]);
            Item item = AssetDatabase.LoadAssetAtPath<Item>(assetPath);
            if (item != null)
            {
                items[i] = item;
            }
        }
        Item randItem;
        randItem = items[rand.Next(0, items.Length)];
        try
        {
            ItemManager itemManager = GameObject.Find("Item Canvas").GetComponent<ItemManager>();
            itemManager.addItem(randItem);
        }
        catch (Exception e) 
        {
            Debug.Log("Could not add player item");
            Debug.Log(e);
        }
        loadRound();
    }
}
