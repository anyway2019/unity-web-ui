using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class Pagination:MonoBehaviour
{
    public Transform content;
    public GameObject moreBtnPrefab;
    public GameObject preBtnPrefab;
    public GameObject nextBtnPrefab;
    public GameObject btnPrefab;
    public int PageIndex { get; private set; }
    public int PageSize { get; private set; }
    public int Total { get; private set; }

    private Action<int> CallBack { get; set; }
    
    public Pagination SetPageSize(int pageSize = 10)
    {
        this.PageSize = pageSize;
        return this;
    }
    public Pagination SetPageIndex(int pageIndex = 0)
    {
        this.PageIndex = pageIndex;
        return this;
    }
    public Pagination SetPageTotal(int total = 1)
    {
        this.Total = total;
        return this;
    }

    private void Refresh()
    {
        Utils.DestroyChildren(content);

        RenderPreviousButton();

        for (int i = 0; i < Total; i++)
        {
            if (PageIndex - 0 >= 4 && i < PageIndex)
            {
                if (i == 0 || PageIndex - 1 == i || PageIndex - 2 == i)
                {
                    RenderPageButton(i);
                }

                if (i == 1)
                {
                    RenderMoreButton();
                }

                continue;
            }

            if (Total - PageIndex - 1 >= 4 && i >= PageIndex)
            {
                if (i == Total - 1 || PageIndex == i || PageIndex + 1 == i || PageIndex + 2 == i)
                {
                    RenderPageButton(i);
                }

                if (i == Total - 2)
                {
                    RenderMoreButton();
                }

                continue;
            }

            RenderPageButton(i);
        }

        RenderNextButton();
    }
    public void Start(Action<int> action)
    {
        CallBack = action;
        Refresh();
    }
    private void RenderMoreButton()
    {
        var moreBtn = Instantiate(moreBtnPrefab, content);
        moreBtn.SetActive(true);
    }

    private void RenderPageButton(int pageIndex)
    {
        var btn = Instantiate(btnPrefab, content);
        var script = btn.GetComponent<PaginationButton>();
        script.SetPageIndex(pageIndex);

        if (pageIndex == PageIndex)
        {
            script.ShowLight();
        }
        else
        {
            script.ShowShadow();
        }

        Utils.AddEvent(btn, UnityEngine.EventSystems.EventTriggerType.PointerClick,
             (e) => { Redirect(pageIndex); });
    }

    private void RenderNextButton()
    {
        var nextBtn = Instantiate(nextBtnPrefab, content);
        nextBtn.SetActive(true);

        Utils.AddEvent(nextBtn, UnityEngine.EventSystems.EventTriggerType.PointerClick, (e) => { Next(); });
    }

    private void RenderPreviousButton()
    {
        var preBtn = Instantiate(preBtnPrefab, content);
        preBtn.SetActive(true);

        Utils.AddEvent(preBtn, UnityEngine.EventSystems.EventTriggerType.PointerClick, (e) => { Previous(); });
    }

    //上一页
    private void Previous()
    {
        var newIndex = PageIndex - 1;
        var index = newIndex < 0 ? 0 : newIndex;
        if (index != PageIndex)
        {
            PageIndex = index;
            CallBack?.Invoke(PageIndex);
            Refresh();
        }
    }

    //下一页
    private void Next()
    {
        var newIndex = PageIndex + 1;
        var index = newIndex >= Total ? Total - 1 : newIndex;
        if (index != PageIndex)
        {
            PageIndex = index;
            CallBack?.Invoke(PageIndex);
            Refresh();
        }
    }

    //指定到第几页
    private void Redirect(int index)
    {
        var list = FindObjectsOfType<PaginationButton>();
        foreach (var item in list)
        {
            if (item.PageIndex == index)
            {
                item.ShowLight();
            }
            else
            {
                item.ShowShadow();
            }
        }

        PageIndex = index;
        CallBack?.Invoke(PageIndex);
        Refresh();
    }
}

