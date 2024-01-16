using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI bodyField;

    private LayoutElement layoutElement;
    private RectTransform rectTransform;
    private VerticalLayoutGroup verticalLayoutGroup;

    public int prefWidth;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        layoutElement = GetComponent<LayoutElement>();
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
    }

    private void Start()
    {
        layoutElement.preferredWidth = prefWidth;
    }
    private void Update()
    {
        Vector2 position = Input.mousePosition;
        transform.position = position;
        if (position.x + prefWidth > Screen.width)
        {
            rectTransform.pivot = new Vector2(1,0);
            verticalLayoutGroup.padding.right = 45;
            verticalLayoutGroup.padding.left = 10;
        }
        else
        {
            rectTransform.pivot = new Vector2(0, 1);
            verticalLayoutGroup.padding.right = 10;
            verticalLayoutGroup.padding.left = 45;
        }

    }

    public void setText(string body, string header) 
    {
        if (string.IsNullOrEmpty(header))
        { 
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }
        bodyField.text = body;
    }
}
