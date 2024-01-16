using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class TooltipSystem : MonoBehaviour
{
    // Start is called before the first frame update

    private static TooltipSystem current;

    public Tooltip tooltip;
    public void Awake()
    {
        current = this;
    }

    public static void Show(string body, string header = "") 
    {
        current.tooltip.setText(body, header);
        current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.tooltip.gameObject.SetActive(false);
    }
}
