using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName = "";
    public string itemEffect = "";
    public Sprite itemImage;
    public string runType = "";
    public int uses = -1;
}
