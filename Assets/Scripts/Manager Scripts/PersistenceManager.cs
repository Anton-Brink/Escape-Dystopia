using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenceManager : MonoBehaviour, StatObserver
{

    [SerializeField] private Subject subject;

    private void Awake()
    {
        if (PlayerScript.Instance == null)
        {
            Debug.LogError("PlayerScript instance is not initialized. Make sure PlayerScript is in the scene and initialized.");
            return;
        }
        subject = PlayerScript.Instance;
    }
    public void statChange(int changeAmount, string statName)
    {
        PlayerPrefs.SetInt(statName, changeAmount);
        PlayerPrefs.Save();
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        subject.AddStatObserver(this);
    }

    private void OnDisable()
    {
        subject.RemoveStatObserver(this);
    }
}
