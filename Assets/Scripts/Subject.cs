using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private List<StatObserver> statObservers = new List<StatObserver>();

    public void AddStatObserver(StatObserver observer) 
    {
        statObservers.Add(observer);
    }
    public void RemoveStatObserver(StatObserver observer)
    {
        statObservers.Remove(observer);
    }
    protected void NotifyStatObservers(int changeAmount, string statName)
    {
        Debug.Log("enter notify stat observers");
        Debug.Log(changeAmount);
        Debug.Log(statName);
        statObservers.ForEach(observer => observer.statChange(changeAmount, statName));
    }
}
