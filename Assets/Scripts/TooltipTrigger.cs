using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public string header;
    public string body;
    public void OnPointerEnter(PointerEventData eventData) 
    {
        TooltipSystem.Show(body, header);
    }

    public void removeTooltip()
    {
        gameObject.SetActive(false);
        TooltipSystem.Hide();
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }




}
