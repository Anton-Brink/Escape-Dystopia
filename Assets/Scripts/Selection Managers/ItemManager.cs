using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{

    public List<Item> playerItems = new List<Item>();
    Vector2 imageSize = new Vector2(40f, 40f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ran at the start of each stage and when entering new "zone" to remove items that should be removed after a certain event takes place
    public void updateItems()
    {
        
    }

    // add item to top of screen and to player items so it can be used or viewed
    public void addItem(Item item) 
    {
        Vector2 imageSize = new Vector2(0.4f, 0.4f);
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
        }
        else
        {
            Debug.Log("Could not retrieve item image");
            // add code for default image
        }
    }
    // check which items have a per turn effect and run them
    public void applyPerTurnItemEffects()
    {
        
    }
    // use consumable item and remove from inventory
    public void applyConsumableItemEffect() 
    {
        //apply code
    }
}
