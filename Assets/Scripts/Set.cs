using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class Set : ScriptableObject
{
    public string setName = "";
    public string setDescription;
    public Sprite setImage;
    public string[] setOptions;
    public Card[] setCards;
    public int cardLimit = 1;
    public Color components = Color.red;
    public bool setUnlocked = true;
}
