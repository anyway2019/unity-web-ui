using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaginationDemo : MonoBehaviour
{
    private Pagination manager { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Pagination>();
        manager.SetPageIndex(0).SetPageSize(10).SetPageTotal(10).Start((e) =>
        {
            Debug.Log($"当前在第{e+1}页！");//索引从0开始
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
