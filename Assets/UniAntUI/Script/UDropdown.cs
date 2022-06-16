using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UDropdown : UComponentBase, IPointerClickHandler
{
    private int mSelectedIndex = 0;
    public int SelectedIndex
    {
        get
        {
            return this.mSelectedIndex;
        }
    }

    public string SelectedItem
    {
        get
        {
            if (this.mSelectedIndex >= 0)
            {
                return this.GetComponent<Dropdown>().options[this.mSelectedIndex].text;
            }
            return null;
        }
    }

    private UnityAction<int> mOnValueChange;
    public UnityAction<int> OnValueChange
    {
        get
        {
            return mOnValueChange;
        }
        set
        {
            mOnValueChange = value;

        }
    }
    private Outline outline;
    void Awake()
    {
        Utils.AddEvent(this.transform.GetComponent<Image>().gameObject, EventTriggerType.Select, (b) =>
        {
            //App.isChanged = true;
        });
        this.GetComponent<Dropdown>().onValueChanged.AddListener(i =>
        {
            mSelectedIndex = i;
            if (OnValueChange != null)
            {
                mOnValueChange(i);
            }
            SetWhite(i);
            outline.enabled = false;

        });
        SetWhite(this.GetComponent<Dropdown>().value);
        outline = transform.GetComponent<Outline>();
    }
    public void SetWhite(int index)
    {
        if (index != -1 && this.GetComponent<Dropdown>().interactable)
        {
            this.GetComponent<Dropdown>().captionText.color = Utils.HexToColorHtml("#ffffff");
        }
        if (!GetComponent<Dropdown>().interactable)
        {
            this.GetComponent<Dropdown>().captionText.color = Utils.HexToColorHtml("#5c5c6e");
        }
    }
    public void SetValidate()
    {
        outline.enabled = true;
    }

    public void OnSelect(BaseEventData eventData)
    {
        var image = this.transform.GetComponent<Image>();
        var png = Resources.Load<Sprite>("Images/ipt-1");
        image.sprite = png;
    }
    public void OnDeselect(BaseEventData eventData)
    {
        var image = this.transform.GetComponent<Image>();
        var png1 = Resources.Load<Sprite>("Images/ipt-0");
        image.sprite = png1;
    }

    public void AddOptions(List<Dropdown.OptionData> options)
    {
        this.GetComponent<Dropdown>().AddOptions(options);
    }

    public void AddOptions(List<string> options)
    {
        this.GetComponent<Dropdown>().AddOptions(options);
    }

    public void ClearOptions()
    {
        this.GetComponent<Dropdown>().ClearOptions();
    }

    public void SelectIndex(int index)
    {
        this.GetComponent<Dropdown>().value = index;
        this.mSelectedIndex = index;
    }

    public void SelectValue(string value)
    {
        var options = this.GetComponent<Dropdown>().options;
        for (int i = 0; i < options.Count; i++)
        {
            if (options[i].text == value)
            {
                SelectIndex(i);
                return;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GetComponent<Dropdown>().value == -1 || !GetComponent<Dropdown>().interactable) return;
        var x = transform.Find("Dropdown List").GetComponent<ScrollRect>().content;
        x.GetChild(SelectedIndex + 1).Find("Item Label").GetComponent<Text>().color = Utils.HexToColorHtml("#f93086");
    }
}

