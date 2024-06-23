using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Item : ScriptableObject
{
    [SerializeField]  public string itemName = "";
    [SerializeField] public string itemEffect = "";
    [SerializeField] public Sprite itemImage;
    [SerializeField] public string imagePath = "Assets/BackgroundTestSceneAssets/Assets/Images and Icons/Items/Top Hat.png";
    [SerializeField] public string runType = "";
    [SerializeField] public int uses = -1;
    [SerializeField] public int price = 100;

}
