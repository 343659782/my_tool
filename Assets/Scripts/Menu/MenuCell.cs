using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class MenuCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private GameObject _selectImg;
    public bool Selected = false;
    public string Key = "";
    public MenuGrid Back = null;
    public MenuGrid Belong = null;
    public MenuGrid Next = null;
    private Text _text;
    private Action<MenuCell> _enterCb;
    private Action<MenuCell> _exitCb;
    private Action<MenuCell> _clickCb;
    private void Awake()
    {
        this._text = this.transform.Find("Text").GetComponent<Text>();
        this._selectImg = this.transform.Find("Select").gameObject;
        this._selectImg.SetActive(false);
    }

    public void ResetCell()
    {
        this._selectImg.SetActive(false);
    }

    public void FullData(string name, MenuGrid belong, Action<MenuCell> enterCb, Action<MenuCell> exitCb, Action<MenuCell> clickCb)
    {
        this.gameObject.name = name;
        this.Key = name;
        this.Belong = belong;
        this._text.text = name;
        this._enterCb = enterCb;
        this._exitCb = exitCb;
        this._clickCb = clickCb;
    }

    public void FullUI(float width, float height)
    {
        this.GetComponent<LayoutElement>().preferredWidth = width;
        this.GetComponent<LayoutElement>().preferredHeight = height;
    }

    public void SetSelectImgActive(bool active)
    {
        this._selectImg.SetActive(active);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this._enterCb?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this._exitCb?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        this._clickCb?.Invoke(this);
    }
}
