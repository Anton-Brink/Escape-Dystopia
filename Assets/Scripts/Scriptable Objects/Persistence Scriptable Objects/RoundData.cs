using UnityEngine;

[CreateAssetMenu(fileName = "RoundData", menuName = "Custom/RoundData", order = 1)]
public class RoundData : ScriptableObject
{
    public string pathName;
    public int pathRound;
    public string[] path;    // Add any other data you want to save between scenes
}