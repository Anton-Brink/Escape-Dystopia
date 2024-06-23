using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InfoScript : MonoBehaviour
{
    private int money = 0;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI roundText = null;
    public RoundData roundData;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("money"))
        {
            money = PlayerPrefs.GetInt("money");
        }
        moneyText.text = money.ToString();
        setRound();
    }

    public void addMoney(int amountToAdd)
    {
        money += amountToAdd;
        PlayerPrefs.SetInt("money", money);
        updateMoney();
    }

    public void removeMoney(int amountToRemove)
    {
        money -= amountToRemove;
        PlayerPrefs.SetInt("money", money);
        updateMoney();
    }

    private void updateMoney()
    {
        moneyText.text = money.ToString();
    }

    private void setRound()
    {
        if (roundData.pathRound != 0)
        {
            int num = roundData.pathRound;
            string[] romanSymbols = { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C" };
            int[] romanValues = { 1, 4, 5, 9, 10, 40, 50, 90, 100 };

            string romanNumeral = "";
            int index = romanValues.Length - 1;
            while (num > 0)
            {
                if (num >= romanValues[index])
                {
                    romanNumeral += romanSymbols[index];
                    num -= romanValues[index];
                }
                else
                {
                    index--;
                }
            }
            if (roundText != null)
            {
                roundText.text = romanNumeral;
            }            
        }
    }
}
