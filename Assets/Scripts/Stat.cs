using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class Stat : ScriptableObject
{
    public string statName = "";
    public string statDescription = "";
    public Sprite statImage;
    public int[] statArgument = { };
    public string[] statType = { };
}
