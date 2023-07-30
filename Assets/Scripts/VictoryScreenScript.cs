using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreenScript : MonoBehaviour
{

    public GameObject sceneManager;
    SceneManagerScript sceneManagerScript;
    // used to handle the victory screen interaction such as character selection and closing it
    public void confirmSelection()
    {
        sceneManagerScript = sceneManager.GetComponent<SceneManagerScript>();
        gameObject.SetActive(false);
        sceneManagerScript.loadStartFight();
        //sceneManagerScript.transition();

    }
}
