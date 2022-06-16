using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Timers;

[RequireComponent(typeof(Button))]
public class UButton : UComponentBase
{
    public enum Type
    {
        Normal,
        Border,
        Primary,
        Image
    }

    [SerializeField] Type mType;

    private Image image;
    private Text text;
    private Outline outline;
    public Button Button { get; set; }

    protected void Awake()
    {
        outline = this.GetComponent<Outline>();
        if (mType == Type.Border && outline == null)
        {
            outline = this.gameObject.AddComponent<Outline>();
            outline.effectDistance = new Vector2(2, 2);
        }

        image = gameObject.GetComponent<Image>();
        text = this.transform.Find("Text").GetComponent<Text>();
        if (mType == Type.Image)
        {
            text.gameObject.SetActive(false);
        }

        Button = this.GetComponent<Button>();

        InitColor();
        BindEvent();
    }
    private void InitColor()
    {
        text.color = Color.white;
        switch (mType)
        {
            case Type.Normal:
                {
                    image.color = ConvertColor("#414251");
                }
                break;
            case Type.Border:
                {
                    outline.enabled = true;
                    outline.effectColor = ConvertColor("#474759");
                    text.color = ConvertColor("#474759");
                    image.color = ConvertColor("#272732");
                }
                break;
            case Type.Primary:
                {
                    image.color = ConvertColor("#F93086");
                }
                break;
            default:
                break;
        }
    }
    
    private void BindEvent()
    {
        //鼠标进入
        Utils.AddEvent(this.gameObject, EventTriggerType.PointerEnter, new UnityEngine.Events.UnityAction<UnityEngine.EventSystems.BaseEventData>(e =>
        {
            if (!this.isActiveAndEnabled) return;

            switch (mType)
            {
                case Type.Normal:
                    {
                        image.color = ConvertColor("#5C5C6E");
                    }
                    break;
                case Type.Border:
                    {
                        outline.effectColor = Color.white;
                        text.color = ConvertColor("#fff");
                    }
                    break;
                case Type.Primary:
                    {
                        image.color = ConvertColor("#FF528A");
                    }
                    break;
                default:
                    break;
            }
        }));

        //鼠标离开
        Utils.AddEvent(this.gameObject, EventTriggerType.PointerExit, new UnityAction<BaseEventData>(e =>
        {
            if (!this.isActiveAndEnabled) return;

            switch (mType)
            {
                case Type.Normal:
                    {
                        image.color = ConvertColor("#414251");
                    }
                    break;
                case Type.Border:
                    {
                        outline.effectColor = ConvertColor("#474759");
                        text.color = ConvertColor("#474759");
                    }
                    break;
                case Type.Primary:
                    {
                        image.color = ConvertColor("#F93086");
                    }
                    break;
                default:
                    break;
            }
        }));

        //按下
        Utils.AddEvent(this.gameObject, EventTriggerType.PointerDown, new UnityEngine.Events.UnityAction<BaseEventData>(e =>
        {
            if (!this.isActiveAndEnabled) return;

            switch (mType)
            {
                case Type.Normal:
                    {
                        image.color = ConvertColor("#23232D");
                    }
                    break;
                case Type.Border:
                    {
                        outline.effectColor = ConvertColor("#353543");
                        text.color = ConvertColor("#414251");
                        image.color = ConvertColor("#23232D");
                    }
                    break;
                case Type.Primary:
                    {
                        image.color = ConvertColor("#BD2255");
                    }
                    break;
                case Type.Image:
                    {

                    }
                    break;
                default:
                    break;
            }
        }));

        //松开
        Utils.AddEvent(this.gameObject, EventTriggerType.PointerUp, new UnityEngine.Events.UnityAction<BaseEventData>(e =>
        {
            if (!this.isActiveAndEnabled) return;

            switch (mType)
            {
                case Type.Normal:
                    {
                        image.color = ConvertColor("#5C5C6E");
                    }
                    break;
                case Type.Border:
                    {
                        outline.effectColor = Color.white;
                        text.color = Color.white;
                    }
                    break;
                case Type.Primary:
                    {
                        image.color = ConvertColor("#FF528A");
                    }
                    break;
                case Type.Image:
                    {

                    }
                    break;
                default:

                    break;
            }
        }));
    }

    public void SetEnabled(bool value)
    {
        base.enabled = value;
        Button.enabled = value;
        if (base.enabled)
        {
            InitColor();
        }
        else
        {
            text.color = ConvertColor("#353543");
            if (outline != null)
            {
                outline.enabled = false;
            }
            image.color = ConvertColor("#414251");
        }
    }

    private Color ConvertColor(string colorStr)
    {
        ColorUtility.TryParseHtmlString(colorStr, out Color color);

        return color;
    }
}
