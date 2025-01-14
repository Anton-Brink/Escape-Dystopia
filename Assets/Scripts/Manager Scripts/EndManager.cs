using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SceneManager;
    private SceneManagerScript sceneManagerScript;
    private List<Set> playerSets = new List<Set>();
    private List<Item> playerItems = new List<Item>();
    public void endGame() 
    {
        sceneManagerScript = SceneManager.GetComponent<SceneManagerScript>();
        resetEverything();
        sceneManagerScript.loadScene("Path Selection");
    }

    private void resetEverything() 
    {
        JsonFunctions.SaveScriptableObjects(playerItems, "playerItems.txt");
        JsonFunctions.SaveScriptableObjects(playerSets, "playerSets.txt");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

}
