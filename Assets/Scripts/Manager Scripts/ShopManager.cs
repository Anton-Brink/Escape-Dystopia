using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEditor.Progress;
using System;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class ShopManager : MonoBehaviour
{
    public RoundData RoundData;
    private SceneManagerScript sceneManagerScript;
    public GameObject sceneManager;
    public int money = 0;
    private Vector2 imageSize = new Vector2(100f, 100f);
    public GameObject specialItemSection;
    public GameObject itemSection;
    private Font font;
    private ItemManager itemManager;
    private InfoScript infoScript;
    public GameObject infoManager;

    private void Start()
    {
        infoScript = infoManager.GetComponent<InfoScript>();
        font = Font.CreateDynamicFontFromOSFont("Arial", 12);
        setPlayerInformation();
        addShopItems();
    }

    public void loadRound() 
    {
        sceneManagerScript = sceneManager.GetComponent<SceneManagerScript>();
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

    private void setPlayerInformation()
    {
        if (PlayerPrefs.HasKey("money"))
        {
            money = PlayerPrefs.GetInt("money");
        }
        else
        {
            money = 0;
        }
    }

    private void addShopItems()
    {
        //import random so random item can be selected
        System.Random rand = new System.Random();
        //get item options
        Item randItem;
        string[] scriptableItemObjects = AssetDatabase.FindAssets("t:Item", new[] { "Assets/BackgroundTestSceneAssets/Sprites/Items" });
        int itemCount = scriptableItemObjects.Length;
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

        //set random item
        randItem = items[rand.Next(0, items.Length)];
        var totalSpecialShopItems = 1;

        //create shop item component
        GameObject shopItemObject = new GameObject("shopItem" + totalSpecialShopItems);
        shopItemObject.transform.position = new Vector2(0, 0);
        RectTransform shopItemRectTransform = shopItemObject.AddComponent<RectTransform>();
        shopItemRectTransform.sizeDelta = new Vector2(imageSize.x, imageSize.y + 30); //+30 for text
        shopItemObject.transform.SetParent(specialItemSection.transform, false);
        System.Type tooltipScript = System.Type.GetType("TooltipTrigger");
        if (tooltipScript != null)
        {
            shopItemObject.AddComponent(tooltipScript);
            shopItemObject.GetComponent<TooltipTrigger>().header = randItem.itemName;
            shopItemObject.GetComponent<TooltipTrigger>().body = randItem.itemEffect;
        }

        //add image component so I can se background so text is more easily readable
        Image backgroundImage = shopItemObject.AddComponent<Image>();
        backgroundImage.color = Color.gray;

        //add buy button component
        Button buttonComponent = shopItemObject.AddComponent<Button>();
        buttonComponent.onClick.AddListener(() => buyItem(randItem, shopItemObject));

        //create shop item component image
        GameObject imageObject = new GameObject("itemImage" + totalSpecialShopItems);
        Image image = imageObject.AddComponent<Image>();
        image.sprite = randItem.itemImage;
        RectTransform rectTransform = imageObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(imageSize.x, imageSize.y);
        imageObject.transform.SetParent(shopItemObject.transform, false);
        rectTransform.anchorMin = new Vector2(0.5f, 1);
        rectTransform.anchorMax = new Vector2(0.5f, 1);
        rectTransform.anchoredPosition = new Vector2(0, 0);
        rectTransform.pivot = new Vector2(0.5f, 1);

        //create shop item component text
        GameObject priceTextObject = new GameObject("itemPrice" + totalSpecialShopItems);
        Text priceText = priceTextObject.AddComponent<Text>();
        priceText.text = randItem.price.ToString();
        //set text visual settings
        priceText.font = font;
        priceText.fontSize = 20;
        priceText.color = Color.black;
        priceText.alignment = TextAnchor.MiddleCenter;


        RectTransform textRectTransform = priceTextObject.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = new Vector2(imageSize.x, 30);
        textRectTransform.SetParent(shopItemObject.transform, false);
        textRectTransform.anchorMin = new Vector2(0.5f, 1);
        textRectTransform.anchorMax = new Vector2(0.5f, 1);
        textRectTransform.anchoredPosition = new Vector2(0, 0);
        textRectTransform.pivot = new Vector2(0.5f, 1);
        priceTextObject.transform.localPosition = new Vector2(0, -30);

    }

    private void buyItem(Item shopItem, GameObject shopItemGameObject)
    {
        if (money > shopItem.price)
        {
            itemManager = GameObject.Find("Item Canvas").GetComponent<ItemManager>();
            itemManager.addItem(shopItem);
            infoScript.removeMoney(shopItem.price);
            setPlayerInformation();
            TooltipTrigger tooltipComponent = shopItemGameObject.GetComponent<TooltipTrigger>();
            tooltipComponent.removeTooltip();
            Destroy(shopItemGameObject);
        }
        else
        {
            Debug.Log("Insufficient Funds");
        }
    }

}
