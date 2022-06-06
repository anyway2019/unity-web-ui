using System;
using UnityEngine;
using UnityEngine.UI;
public class PaginationButton:MonoBehaviour
{
    public Text PageIndexText;
    public GameObject ClickImage;

    public int PageIndex { get; set; } //pageIndex 0~infinite

    public void SetPageIndex(int pageIndex)
    {
        PageIndex = pageIndex;
        PageIndexText.text = $"{pageIndex + 1}";
    }

    public void ShowLight()
    {
        ClickImage.SetActive(true);
    }

    public void ShowShadow()
    {
        ClickImage.SetActive(false);
    }
}

