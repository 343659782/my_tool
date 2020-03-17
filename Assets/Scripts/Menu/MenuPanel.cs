using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : Singleton<MenuPanel>
{
    private GameObject _gridPrefab;
    private Dictionary<int, MenuGrid> _girdDic = new Dictionary<int, MenuGrid>();

    private List<Player> _selectPlayers = null;

    private MenuGrid _mainGrid;
    private MenuGrid _defaultHpGrid;
    private MenuGrid _thousandGrid;
    private MenuGrid _hundredGrid;

    private void Awake()
    {
        _instance = this;
        this._gridPrefab = Resources.Load<GameObject>("UIPrefabs/Menu/MenuGrid");
    }

    private void Start()
    {
        this.FullData();
        this.Close();
    }

    public void Open(List<Player> selectPlayers)
    {
        this._selectPlayers = selectPlayers;
        this.transform.localPosition = DataUtils.ScreenPosToUIPos(new Vector2(Input.mousePosition.x + 2, Input.mousePosition.y));
    }

    public void Close()
    {
        this.ResetPanel();
        this.transform.localPosition = Vector3.one * 10000;
    }

    public void ResetPanel()
    {
        this._defaultHpGrid.ResetGrid();
        this._defaultHpGrid.gameObject.SetActive(false);
        this._thousandGrid.gameObject.SetActive(false);
        this._hundredGrid.gameObject.SetActive(false);
    }

    private void FullData()
    {
        // manGrid
        this._mainGrid = GameObject.Instantiate(this._gridPrefab).GetComponent<MenuGrid>();
        this._mainGrid.transform.SetParent(this.transform);
        this._mainGrid.transform.localPosition = Vector3.zero;
        this._mainGrid.transform.localScale = Vector3.one;
        this._mainGrid.GetComponent<RectTransform>().pivot = Vector2.up;
        ArrayList mainCells = new ArrayList();
        mainCells.Add(DefineClass.SetDefaultHp);
        mainCells.Add(DefineClass.AddHp);
        mainCells.Add(DefineClass.SubHp);
        mainCells.Add(DefineClass.Die);
        mainCells.Add(DefineClass.AddPurdue);
        mainCells.Add(DefineClass.DeletePurdue);
        this._mainGrid.FullData("MainGrid", mainCells, this.OnPointerEnterCell, this.OnPointerExitCell, this.OnPointerClickCell);

        // defaultGrid
        this._defaultHpGrid = GameObject.Instantiate(this._gridPrefab).GetComponent<MenuGrid>();
        this._defaultHpGrid.transform.SetParent(this.transform);
        this._defaultHpGrid.transform.localScale = Vector3.one;
        this._defaultHpGrid.transform.localPosition = Vector3.right * 90;
        this._defaultHpGrid.GetComponent<RectTransform>().pivot = Vector2.up;

        ArrayList defaultHpCells = new ArrayList();
        defaultHpCells.Add("5200");
        defaultHpCells.Add("6300");
        defaultHpCells.Add("7500");
        defaultHpCells.Add("8300");
        _defaultHpGrid.FullData("DefaultHpGrid", defaultHpCells, this.OnPointerEnterCell, this.OnPointerExitCell, this.OnPointerClickCell);
        _defaultHpGrid.FullUI(60, 30);

        // thousandGrid
        this._thousandGrid = GameObject.Instantiate(this._gridPrefab).GetComponent<MenuGrid>();
        this._thousandGrid.transform.SetParent(this.transform);
        this._thousandGrid.transform.localScale = Vector3.one;
        this._thousandGrid.transform.localPosition = Vector3.right * 90;
        this._thousandGrid.GetComponent<RectTransform>().pivot = Vector2.up;

        ArrayList thousandCells = new ArrayList();
        for (int i = 0; i < 10000; i += 1000)
        {
            thousandCells.Add(i.ToString());
        }
        _thousandGrid.FullData("ThousandGrid", thousandCells, this.OnPointerEnterCell, this.OnPointerExitCell, this.OnPointerClickCell);
        _thousandGrid.FullUI(60, 30);

        // hundredGrid
        this._hundredGrid = GameObject.Instantiate(this._gridPrefab).GetComponent<MenuGrid>();
        this._hundredGrid.transform.SetParent(this.transform);
        this._hundredGrid.transform.localScale = Vector3.one;
        this._hundredGrid.transform.localPosition = Vector3.right * (90 + 60);
        this._hundredGrid.GetComponent<RectTransform>().pivot = Vector2.up;

        ArrayList hundredCells = new ArrayList();
        for (int i = 0; i < 1000; i += 100)
        {
            hundredCells.Add(i.ToString());
        }
        _hundredGrid.FullData("HundredGrid", hundredCells, this.OnPointerEnterCell, this.OnPointerExitCell, this.OnPointerClickCell);
        _hundredGrid.FullUI(60, 30);

        this._mainGrid.CellDic[DefineClass.SetDefaultHp].NextGrid = this._defaultHpGrid;
        this._mainGrid.CellDic[DefineClass.AddHp].NextGrid = this._thousandGrid;
        this._mainGrid.CellDic[DefineClass.SubHp].NextGrid = this._thousandGrid;
        this._mainGrid.CellDic[DefineClass.AddPurdue].NextGrid = this._thousandGrid;
        this._mainGrid.CellDic[DefineClass.DeletePurdue].NextGrid = this._thousandGrid;

        foreach (var item in this._thousandGrid.CellDic.Values)
        {
            item.NextGrid = this._hundredGrid;
        }

    }

    //---------------------------Event-------------------------------
    public MenuGrid CurGrid;
    public MenuCell CurCell;

    public void OnPointerEnterCell(MenuCell cell)
    {
        //Debug.Log("PointerEnterCell  BelongGrid key " + cell.Belong.Key + " EnterCell key:" + cell.Key);
        this.CurGrid = cell.Belong;
        this.CurCell = cell;

        if (cell.Belong == this._mainGrid)
        {
            this._defaultHpGrid.Close();
            this._thousandGrid.Close();
            this._hundredGrid.Close();
            cell.SelectCell();
        }
        else if (cell.Belong == this._defaultHpGrid)
        {

        }
        else if (cell.Belong == this._thousandGrid)
        {
            this._hundredGrid.Close();
            cell.SelectCell();
        }
        else if (cell.Belong == this._hundredGrid)
        {

        }
        cell.Belong.SelectCell(cell);
    }

    public void OnPointerExitCell(MenuCell cell)
    {

    }

    public void OnPointerClickCell(MenuCell cell)
    {
        if (cell.Belong == this._mainGrid)
        {
            this.OnMainCellClick(cell);
        }
        else if (cell.Belong == this._defaultHpGrid)
        {
            this.OnDefaultHpCellClick(cell);
        }
        else if (cell.Belong == this._thousandGrid)
        {
            this.OnThousandCellClick(cell);
        }
        else if (cell.Belong == this._hundredGrid)
        {
            this.OnHundredCellClick(cell);
        }
    }


    //---------------------------Event end-------------------------------

    //---------------------------GridMain-------------------------------
    private void OnMainCellClick(MenuCell cell)
    {
        switch (cell.Key)
        {
            case "Die":
                break;
            default:
                break;
        }
    }

    public void AddHp(int value)
    {
        for (int i = 0; i < this._selectPlayers.Count; i++)
        {
            this._selectPlayers[i].AddHp(value);
        }
    }

    public void SubHp(int value)
    {
        for (int i = 0; i < this._selectPlayers.Count; i++)
        {
            this._selectPlayers[i].SubHp(value);
        }
    }

    public void Die()
    {
        for (int i = 0; i < this._selectPlayers.Count; i++)
        {
            this._selectPlayers[i].Die();
        }
    }

    public void AddPurdue(int value)
    {

    }

    public void DeletePurdue(int value)
    {

    }


    //---------------------------DefaultHp-------------------------------

    private void OnDefaultHpCellClick(MenuCell cell)
    {
        this.SetDefaultMaxHp(int.Parse(cell.Key));
    }

    private void SetDefaultMaxHp(int value)
    {
        Debug.Log("SetDefaultMaxHp:" + value.ToString());
        for (int i = 0; i < this._selectPlayers.Count; i++)
        {
            this._selectPlayers[i].SetDefaultMaxHp(value);
        }
        this.Close();
    }

    //---------------------------Thousand-------------------------------
    private void OnThousandCellClick(MenuCell cell)
    {
        Debug.Log("setValue:" + this._thousandGrid.SelectedCell.Key.ToString());
        int value = int.Parse(this._thousandGrid.SelectedCell.Key);
        for (int i = 0; i < this._selectPlayers.Count; i++)
        {
            switch (this._mainGrid.SelectedCell.Key)
            {
                case DefineClass.AddHp:
                    this._selectPlayers[i].AddHp(value);
                    break;
                case DefineClass.SubHp:
                    this._selectPlayers[i].SubHp(value);
                    break;
                case DefineClass.AddPurdue:
                    this._selectPlayers[i].AddPurdue(value);
                    break;
                case DefineClass.DeletePurdue:
                    this._selectPlayers[i].DeletePurdue(value);
                    break;
                default:
                    break;
            }
        }
        this.Close();

    }


    //---------------------------Hundred-------------------------------
    private void OnHundredCellClick(MenuCell cell)
    {
        Debug.Log("setValue:" + this._thousandGrid.SelectedCell.Key.ToString() + " hundred:" + this._hundredGrid.SelectedCell.Key);
        int value = int.Parse(this._thousandGrid.SelectedCell.Key) + int.Parse(this._hundredGrid.SelectedCell.Key);

        for (int i = 0; i < this._selectPlayers.Count; i++)
        {
            switch (this._mainGrid.SelectedCell.Key)
            {
                case DefineClass.AddHp:
                    this._selectPlayers[i].AddHp(value);
                    break;
                case DefineClass.SubHp:
                    this._selectPlayers[i].SubHp(value);
                    break;
                case DefineClass.AddPurdue:
                    this._selectPlayers[i].AddPurdue(value);
                    break;
                case DefineClass.DeletePurdue:
                    this._selectPlayers[i].DeletePurdue(value);
                    break;
                default:
                    break;
            }
        }
        this.Close();
    }
}
