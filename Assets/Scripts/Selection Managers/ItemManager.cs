using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using static UnityEditor.Progress;

[System.Serializable]
public class ItemManager : MonoBehaviour
{
    [SerializeField] public List<Item> playerItems = new List<Item>();
    private Vector2 imageSize = new Vector2(40f, 40f);
    public GameObject player;
    private PlayerScript playerScript;
    public GameObject setManager;
    private SetManager setManagerScript;
    private List<GameObject> itemGameObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        LoadItemList();
        LoadItemGameObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Item> getPlayerItems() 
    {
        return playerItems;
    }

    //ran at the start of each stage and when entering new "zone" to remove items that should be removed after a certain event takes place
    public void updateItems()
    {
        if (itemGameObjects.Count > 0)
        {
            for (int i = 0; i < itemGameObjects.Count; i++)
            {
                itemGameObjects[i].transform.position = new Vector2((itemGameObjects.Count - 1) * 40 + (-410), 0);
            }
        }
        saveItemsToList();
    }

    // add item to top of screen and to player items so it can be used or viewed
    public void addItem(Item item) 
    {
        playerItems.Add(item);
        int totalItems = playerItems.Count;
        Sprite itemSprite = item.itemImage;
        if (itemSprite != null)
        {
            GameObject imageObject = new GameObject("itemImage" + totalItems);
            Image image = imageObject.AddComponent<Image>();
            image.sprite = itemSprite;
            imageObject.transform.position = new Vector2((totalItems - 1) * 40 + (-410), 0);
            RectTransform rectTransform = imageObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(imageSize.x, imageSize.y);
            imageObject.transform.SetParent(transform, false);
            System.Type tooltipScript = System.Type.GetType("TooltipTrigger");
            if (tooltipScript != null)
            {
                imageObject.AddComponent(tooltipScript);
                imageObject.GetComponent<TooltipTrigger>().header = item.itemName;
                imageObject.GetComponent<TooltipTrigger>().body = item.itemEffect;
            }
            if (item.runType == "consumableItem")
            {
                Button buttonComponent = imageObject.AddComponent<Button>();
                buttonComponent.onClick.AddListener(() => applyConsumableItemEffect(item, imageObject));
            }
            itemGameObjects.Add(imageObject);
        }
        else
        {
            Debug.Log("Could not retrieve item image");
            // add code for default image
        }
        saveItemsToList();
    }
    // check which items have a per turn effect and run them
    public void applyPerTurnItemEffects()
    {
        playerScript = player.GetComponent<PlayerScript>();
        for (int i = 0; i < playerItems.Count; i++)
        {
            if (playerItems[i].runType == "repeatItem")
            {
                switch (playerItems[i].itemName)
                {
                    case "More Power":
                        playerScript.setMaxPower(playerScript.getMaxPower() + 1);
                        break;
                    case "More Stats":
                        break;
                    default: break;
                }
            }
        }
    }
    // use consumable item and remove from inventory
    public void applyConsumableItemEffect(Item item, GameObject itemGameObject) 
    {
        setManagerScript = setManager.GetComponent<SetManager>();
        playerScript = player.GetComponent<PlayerScript>();
        switch (item.name)
        {
            case "Bloody Machine":
                playerScript.setMaxHealth(playerScript.getMaxHealth() - 10);
                List<Set> playerSets = setManagerScript.getPlayerSets();
                for (int i = 0; i < playerSets.Count; i++)
                {
                    if (playerSets[i].cardLimit < playerSets[i].setCards.Length)
                    {
                        setManagerScript.updateSetCardLimit(playerSets[i]);
                    }
                }
                removeItem(item, itemGameObject);
                break;
            default : break;
        }
    }

    public void removeItem(Item item, GameObject itemGameObject) 
    {
        if (playerItems.Contains(item))
        {
            // Remove the item from the list
            playerItems.Remove(item);
            itemGameObjects.Remove(itemGameObject);
            TooltipTrigger tooltipComponent = itemGameObject.GetComponent<TooltipTrigger>();
            tooltipComponent.removeTooltip();
            Destroy(itemGameObject);
            updateItems();
        }
    }

    public void saveItemsToList()
    {
        JsonFunctions.SaveScriptableObjects(playerItems, "playerItems.txt");
    }

    public void LoadItemList()
    {
        string filePath = Application.persistentDataPath + "/playerItems.txt";
        if (File.Exists(filePath))
        {
            playerItems = JsonFunctions.LoadScriptableObjects<Item>("playerItems.txt");
        }
        else
        {
            Debug.Log("Path does not exist");
            playerItems = new List<Item>();
        }
    }

    private void LoadItemGameObjects() 
    {
        if (playerItems.Count > 0)
        {
            int totalItems = 0;
            foreach (Item item in playerItems)
            {
                totalItems++;
                Sprite itemSprite = item.itemImage;
                if (itemSprite != null)
                {
                    GameObject imageObject = new GameObject("itemImage" + totalItems);
                    Image image = imageObject.AddComponent<Image>();
                    try
                    {
                        image.sprite = itemSprite;
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e);
                    }
                    imageObject.transform.position = new Vector2((totalItems - 1) * 40 + (-410), 0);
                    RectTransform rectTransform = imageObject.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(imageSize.x, imageSize.y);
                    imageObject.transform.SetParent(transform, false);
                    System.Type tooltipScript = System.Type.GetType("TooltipTrigger");
                    if (tooltipScript != null)
                    {
                        imageObject.AddComponent(tooltipScript);
                        imageObject.GetComponent<TooltipTrigger>().header = item.itemName;
                        imageObject.GetComponent<TooltipTrigger>().body = item.itemEffect;
                    }
                    if (item.runType == "consumableItem")
                    {
                        Button buttonComponent = imageObject.AddComponent<Button>();
                        buttonComponent.onClick.AddListener(() => applyConsumableItemEffect(item, imageObject));
                    }
                    itemGameObjects.Add(imageObject);
                }
                else
                {
                    Debug.Log("Could not retrieve item image");
                    // add code for default image
                }
            }
        }
    }
}
