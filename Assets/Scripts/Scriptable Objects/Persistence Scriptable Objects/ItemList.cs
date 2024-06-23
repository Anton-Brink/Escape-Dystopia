using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class ItemList : ScriptableObject
{
    public List<Item> items = new List<Item>();
}