using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class Utils
{
    static Texture2D cursor;
    public static void DestroyChildren(this Transform target)
    {
        bool isPlaying = Application.isPlaying;
        while (target.childCount != 0)
        {
            Transform child = target.GetChild(0);
            if (isPlaying)
            {
                child.SetParent(null);
                UnityEngine.Object.Destroy(child.gameObject);
            }
            else UnityEngine.Object.DestroyImmediate(child.gameObject);
        }
        Resources.UnloadUnusedAssets();
    }

    public static Color HexToColorHtml(string hex)
    {
        return Color.blue;
    }

    public static void AddEvent(GameObject gameObject, EventTriggerType eventTriggerType, UnityAction<BaseEventData> call)
    {
        EventTrigger.Entry pointerEvent = null;
        EventTrigger et = gameObject.GetComponent<EventTrigger>();
        if (et == null)
        {
            et = gameObject.AddComponent<EventTrigger>();
        }
        foreach (EventTrigger.Entry entry2 in et.triggers)
        {
            if(entry2.eventID == eventTriggerType)
            {
                pointerEvent = entry2;
                break;
            }
        }
        if (pointerEvent == null)
        {
            pointerEvent = new EventTrigger.Entry();
            pointerEvent.eventID = eventTriggerType;
            et.triggers.Add(pointerEvent);
        }

        pointerEvent.callback.AddListener((e)=> {
            if (gameObject == null || gameObject.activeInHierarchy == false) return;
            var btn = gameObject.GetComponent<Button>();
            if(btn != null && btn.enabled == false)
            {
                return;
            }
            call(e);
        });

        if (eventTriggerType == EventTriggerType.PointerClick)
        {
            //设置光标
            if (cursor == null)
            {
                cursor = Resources.Load<Texture2D>("Image/PointerButtonHover");
            }
            AddEvent(gameObject, EventTriggerType.PointerEnter, (e) =>
            {
                Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
            });
            AddEvent(gameObject, EventTriggerType.PointerUp, (e) =>
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            });
            AddEvent(gameObject, EventTriggerType.PointerExit, (e) =>
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            });
           
        }
        //处理EventTrigger 拦截scroll事件触发问题
        var scrollView = gameObject.GetComponentInParent<ScrollRect>();
        if (scrollView != null)
        {
            EventTrigger.Entry entryBegin = new EventTrigger.Entry(), entryDrag = new EventTrigger.Entry(), entryEnd = new EventTrigger.Entry(), entrypotential = new EventTrigger.Entry()
            , entryScroll = new EventTrigger.Entry();

            entryBegin.eventID = EventTriggerType.BeginDrag;
            entryBegin.callback.AddListener((data) => { scrollView.OnBeginDrag((PointerEventData)data); });
            et.triggers.Add(entryBegin);

            entryDrag.eventID = EventTriggerType.Drag;
            entryDrag.callback.AddListener((data) => { scrollView.OnDrag((PointerEventData)data); });
            et.triggers.Add(entryDrag);

            entryEnd.eventID = EventTriggerType.EndDrag;
            entryEnd.callback.AddListener((data) => { scrollView.OnEndDrag((PointerEventData)data); });
            et.triggers.Add(entryEnd);

            entrypotential.eventID = EventTriggerType.InitializePotentialDrag;
            entrypotential.callback.AddListener((data) => { scrollView.OnInitializePotentialDrag((PointerEventData)data); });
            et.triggers.Add(entrypotential);

            entryScroll.eventID = EventTriggerType.Scroll;
            entryScroll.callback.AddListener((data) => { scrollView.OnScroll((PointerEventData)data); });
            et.triggers.Add(entryScroll);
        }
    }
}
