using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public int cardDamage = 4;
    public string damageType = "single";
    public Sprite cardImage;
    public int powerCost = 0;
    public string cardName = "";
}
