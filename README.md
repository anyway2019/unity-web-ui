# UniAntUI
#### A Unity UGUI Framework,inspire by https://www.antdv.com/components/overview


## Pagination[分页控件]
```C#
private Pagination manager { get; set; }

void Start()
{
    manager = FindObjectOfType<Pagination>();
    manager.SetPageIndex(0).SetPageSize(10).SetPageTotal(10).Start((e) =>
    {
        Debug.Log($"当前在第{e+1}页！");//索引从0开始
    });
}
```
![demo](http://rd25qwsml.hd-bkt.clouddn.com/github/paginantiondemo.png)
