using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InfoScript : MonoBehaviour
{
    public GameObject[] items;
    private Vector2 placePosition = new Vector2(-12f,4.5f);
    private int money = 0;
    private int stage = 0;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI stageText;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("addItem", 5 , 5);
        moneyText.text = money.ToString();
        addMoney(10);
        updateStage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addItem(GameObject itemInfo) 
    {
        
    }
    public void addItem()
    {
        Instantiate(items[0], placePosition, Quaternion.identity);
        placePosition.x += 2;
    }

    public void updateStage()
    {
        stage++;
        int stageValue = stage;
        int[] values = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
        String[] romanLetters = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
        string romanStage = "";
        for (int i = 0; i < values.Length; i++)
        {
            while (stageValue >= values[i])
            {
                stageValue = stageValue - values[i];
                romanStage += romanLetters[i];
            }
        }
        stageText.text = romanStage;
    }
    public void addMoney(int amountToAdd)
    {
        money += amountToAdd;

        moneyText.text = money.ToString();
    }
}
