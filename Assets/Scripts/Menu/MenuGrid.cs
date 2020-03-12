using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class MenuGrid : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject _cellPrefab;
    public string Key = "";
    public Dictionary<string, MenuCell> CellDic = new Dictionary<string, MenuCell>();
    private void Awake()
    {
        this._cellPrefab = Resources.Load<GameObject>("UIPrefabs/Menu/MenuCell");
    }

    public void FullData(string name, ArrayList cells, Action<MenuCell> cellEnterCb, Action<MenuCell> cellExitCb, Action<MenuCell> cellClickCb)
    {
        this.CellDic.Clear();
        this.gameObject.name = name;
        this.Key = name;
        for (int i = 0; i < cells.Count; i++)
        {
            MenuCell cell = GameObject.Instantiate(this._cellPrefab).GetComponent<MenuCell>();
            cell.transform.SetParent(this.transform);
            cell.transform.localPosition = Vector3.zero;
            cell.transform.localScale = Vector3.one;
            cell.FullData(cells[i].ToString(), this, cellEnterCb, cellExitCb, cellClickCb);
            this.CellDic.Add(cells[i].ToString(), cell);
        }
    }

    public void FullUI(float width, float height)
    {
        foreach (var item in this.CellDic.Values)
        {
            item.FullUI(width, height);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void RefreshSelectImg(MenuCell selectCell)
    {
        foreach (var item in this.CellDic.Values)
        {
            item.SetSelectImgActive(item == selectCell);
        }
    }

    public void ResetGrid()
    {
        foreach (var item in this.CellDic.Values)
        {
            item.ResetCell();
        }
    }
}
