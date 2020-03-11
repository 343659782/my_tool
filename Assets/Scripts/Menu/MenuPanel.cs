using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : Singleton<MenuPanel>
{

    private Dictionary<int, MenuGrid> _girdDic = new Dictionary<int, MenuGrid>();

    private Player[] _players = null;

    private GameObject _gridMain;
    private GameObject _gridDefaultHp;


    private void Awake()
    {
        _instance = this;
        this.InitUI();
        this.Close();
    }

    private void InitUI()
    {
        this._gridMain = this.transform.Find("GridMain").gameObject;
        this._gridDefaultHp = this.transform.Find("GridDefaultHp").gameObject;
        this._gridDefaultHp.SetActive(false);
    }

    public void Open(Player[] players)
    {
        this._players = players;
        this.transform.localPosition = DataUtils.ScreenPosToUIPos(new Vector2(Input.mousePosition.x + 2, Input.mousePosition.y));
    }

    public void Close()
    {
        this._gridDefaultHp.SetActive(false);
        this.transform.localPosition = Vector3.one * 10000;
    }

    private void FullData()
    {
    }

    private void CreateFirstMenuGrid()
    {

    }

    //---------------------------GridMain-------------------------------
    public void OnGridMainEnterCell(GameObject obj)
    {
        this._gridDefaultHp.SetActive(false);

        if (obj.name == "setDefaultHp")
        {
            this._gridDefaultHp.SetActive(!this._gridDefaultHp.activeSelf);
        }
    }

    public void SetDefaultMaxHp(int value)
    {
        for (int i = 0; i < this._players.Length; i++)
        {
            this._players[i].SetDefaultMaxHp(value);
        }
        this.Close();
    }

    public void AddHp(int value)
    {
        for (int i = 0; i < this._players.Length; i++)
        {
            this._players[i].AddHp(value);
        }
    }

    public void SubHp(int value)
    {
        for (int i = 0; i < this._players.Length; i++)
        {
            this._players[i].SubHp(value);
        }
    }

    public void Die()
    {
        for (int i = 0; i < this._players.Length; i++)
        {
            this._players[i].Die();
        }
    }

    public void AddPurdue(int value)
    {

    }

    public void DeletePurdue(int value)
    {

    }

    //---------------------------GridMain end-------------------------------
}
