using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu]
public class Enemy : ScriptableObject
{
    public int enemyHealth = 20;
    public int enemyDamage = 5;
    public string enemyName = "";
}
