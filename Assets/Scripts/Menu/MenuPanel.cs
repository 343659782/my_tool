using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : Singleton<MenuPanel>
{
    private GameObject _gridPrefab;
    private Dictionary<int, MenuGrid> _girdDic = new Dictionary<int, MenuGrid>();

    private Player[] _selectPlayers = null;

    private MenuGrid _mainGrid;
    private MenuGrid _defaultHpGrid;
    private MenuGrid _thousandGrid;
    private MenuGrid _hundredGrid;

    private void Awake()
    {
        _instance = this;
        this._gridPrefab = Resources.Load<GameObject>("UIPrefabs/Menu/MenuGrid");
        this.Close();
    }

    private void Start()
    {
        this.FullData();
        this.ResetPanel();
    }

    public void Open(Player[] selectPlayers)
    {
        this._selectPlayers = selectPlayers;
        this.transform.localPosition = DataUtils.ScreenPosToUIPos(new Vector2(Input.mousePosition.x + 2, Input.mousePosition.y));
    }

    public void Close()
    {
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
        mainCells.Add("SetDefaultHp");
        mainCells.Add("AddHp");
        mainCells.Add("SubHp");
        mainCells.Add("Die");
        mainCells.Add("AddPurdue");
        mainCells.Add("DeletePurdue");
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
            this._defaultHpGrid.gameObject.SetActive(false);
            this._thousandGrid.gameObject.SetActive(false);
            this._hundredGrid.gameObject.SetActive(false);
            switch (cell.Key)
            {
                case "SetDefaultHp":
                    this._defaultHpGrid.gameObject.SetActive(!this._defaultHpGrid.gameObject.activeSelf);
                    break;
                case "AddHp":
                    this._thousandGrid.gameObject.SetActive(!this._thousandGrid.gameObject.activeSelf);
                    break;
                case "SubHp":
                    this._thousandGrid.gameObject.SetActive(!this._thousandGrid.gameObject.activeSelf);
                    break;
                case "AddPurdue":
                    this._thousandGrid.gameObject.SetActive(!this._thousandGrid.gameObject.activeSelf);
                    break;
                case "DeletePurdue":
                    this._thousandGrid.gameObject.SetActive(!this._thousandGrid.gameObject.activeSelf);
                    break;
                default:
                    break;
            }
        }
        else if (cell.Belong == this._defaultHpGrid)
        {

        }
        else if (cell.Belong == this._thousandGrid)
        {
            this._hundredGrid.gameObject.SetActive(true);
        }
        else if (cell.Belong == this._hundredGrid)
        {

        }
        cell.Belong.RefreshSelectImg(cell);
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
        for (int i = 0; i < this._selectPlayers.Length; i++)
        {
            this._selectPlayers[i].AddHp(value);
        }
    }

    public void SubHp(int value)
    {
        for (int i = 0; i < this._selectPlayers.Length; i++)
        {
            this._selectPlayers[i].SubHp(value);
        }
    }

    public void Die()
    {
        for (int i = 0; i < this._selectPlayers.Length; i++)
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

    //---------------------------GridMain end-------------------------------

    //---------------------------DefaultHp-------------------------------

    private void OnDefaultHpCellClick(MenuCell cell)
    {
        this.SetDefaultMaxHp(int.Parse(cell.Key));
    }

    private void SetDefaultMaxHp(int value)
    {
        Debug.Log("SetDefaultMaxHp:" + value.ToString());
        for (int i = 0; i < this._selectPlayers.Length; i++)
        {
            this._selectPlayers[i].SetDefaultMaxHp(value);
        }
        this.Close();
    }
    //---------------------------DefaultHp end-------------------------------

}
