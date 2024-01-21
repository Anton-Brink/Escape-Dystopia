using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    Item[] onceOffItems;
    Item[] perTurnItems;
    Item[] usableItems;
    Item[] perStageMissionItems;
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
