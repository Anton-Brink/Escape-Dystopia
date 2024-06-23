using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PathScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject path1;
    public GameObject path2;
    public Sprite lizard;
    public Sprite questionMark;
    public Sprite shop;
    public Sprite rest;
    public Sprite humanBoss;
    public Sprite lizardBoss;
    public Sprite inmate;
    public Sprite humanElite;
    public Sprite lizardElite;
    private Vector2 imageSize = new Vector2(100f, 100f);
    private string[] pathTypes = { "Human", "Lizard" };
    private string[] selectedPathOrder = { };
    private GameObject sceneManager;
    private SceneManagerScript sceneManagerScript;
    public RoundData pathData;


    void Start()
    {
        sceneManager = GameObject.Find("Scene Manager");
        sceneManagerScript = sceneManager.GetComponent<SceneManagerScript>();

        for (int i = 0; i < pathTypes.Length; i++)
        {
            pathGuiFunction(pathTypes[i]);
        }

        void pathGuiFunction(string pathType)
        {
            List<string> pathOrder = createPath();
            for (int i = 0; i < pathOrder.Count; i++)
            {
                GameObject imageObject = new GameObject("pathImage" + i);
                Image image = imageObject.AddComponent<Image>();
                switch (pathOrder[i])
                {
                    case "Normal":
                        if (pathType == "Human")
                        {
                            image.sprite = inmate;
                        }
                        else if (pathType == "Lizard")
                        {
                            image.sprite = lizard;
                        }
                        break;
                    case "Elite":
                        if (pathType == "Human")
                        {
                            image.sprite = humanElite;
                        }
                        else if (pathType == "Lizard")
                        {
                            image.sprite = lizardElite;
                        }
                        break;
                    case "Shop":
                        image.sprite = shop;
                        break;
                    case "Rest":
                        image.sprite = rest;
                        break;
                    case "Boss":
                        if (pathType == "Human")
                        {
                            image.sprite = humanBoss;
                        }
                        else if (pathType == "Lizard")
                        {
                            image.sprite = lizardBoss;
                        }
                        break;
                    default: image.sprite = inmate; break;
                }
                //padding calculation

                int padding = ((1500 - (1500 - pathOrder.Count * 120)) / 2) - 60; //deduct 60 because half an image space has to be deducted since images are placed from the center and not from the left 

                //690 because it starts placing images from the middle so half the imagesize has to be deducted from the total width of the available space per image
                imageObject.transform.position = new Vector2(i * 120 - padding, 0);
                RectTransform rectTransform = imageObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(imageSize.x, imageSize.y);
                if (pathType == "Human") imageObject.transform.SetParent(path1.transform, false);
                else if (pathType == "Lizard") imageObject.transform.SetParent(path2.transform, false);
                System.Type tooltipScript = System.Type.GetType("TooltipTrigger");
                if (tooltipScript != null)
                {
                    imageObject.AddComponent(tooltipScript);
                    imageObject.GetComponent<TooltipTrigger>().header = pathOrder[i];
                }
            }
            if (pathType == "Human")
            {
                Button buttonComponent = path1.AddComponent<Button>();
                buttonComponent.onClick.AddListener(() => setSelection(pathOrder, pathType));
            }
            else
            {
                Button buttonComponent = path2.AddComponent<Button>();
                buttonComponent.onClick.AddListener(() => setSelection(pathOrder, pathType));
            }
        }
    }

    public void setSelection(List<string> pathOrder, string pathType) 
    {
        selectedPathOrder = pathOrder.ToArray();
        pathData.path = selectedPathOrder;
        pathData.pathName = pathType;
        pathData.pathRound = 0;
    }

    public void selectPath() 
    {
        if (selectedPathOrder.Length > 0)
        {
            sceneManagerScript.loadScene("Combat");
        }
    }

    public List<string> createPath()
    {
        //calculate order
        List<string> path = new List<string>();
        List<int> takenPostions = new List<int>();
        do
        {
            int randomNumber = Mathf.RoundToInt(Random.Range(3, 10));
            if(takenPostions.IndexOf(randomNumber) == -1) takenPostions.Add(randomNumber);
        } while (takenPostions.Count < 4);

        int randElitePosition = takenPostions[0];
        int randShopPosition = takenPostions[1];
        int randRestPosition = takenPostions[2];
        int randRestPosition2 = takenPostions[3];
        for (int i = 0; i < 11; i++)
        {
            if (randElitePosition == i) path.Add("Elite");
            else if (randShopPosition == i) path.Add("Shop");
            else if (randRestPosition == i || randRestPosition2 == i) path.Add("Rest");
            else path.Add("Normal");
        }
        path.Add("Boss");
        return path;
    }
}
