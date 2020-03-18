using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarManager : Singleton<WarManager>
{
    private GameObject _cellPrefab;
    private GameObject[][] _gridList;
    private GameObject _gridParent;

    private GameObject _ourPlayerParent;
    private GameObject _theirPlayerParent;
    private GameObject _playerPrefab;

    private GameObject _petPrefab;

    // 位置顺序
    private Dictionary<int, Player> _theirPlayerDic = new Dictionary<int, Player>();
    public Dictionary<int, Player> TheirPlayerDic { get { return this._theirPlayerDic; } }

    private Dictionary<int, Player> _ourPlayerDic = new Dictionary<int, Player>();
    public Dictionary<int, Player> OurPlayerDic { get { return this._ourPlayerDic; } }

    private StanceBase _theirStance = new NormalStance();
    public StanceBase TheirStance { get { return this._theirStance; } set { _theirStance = value; } }

    private StanceBase _ourStance = new NormalStance();
    public StanceBase OurStance { get { return this._ourStance; } set { _ourStance = value; } }

    private int _levelGroup = 69;
    public int LevelGroup { get { return this._levelGroup; } set { _levelGroup = value; } }

    private int _curRound = 1;
    public int CurRound { get { return this._curRound; } set { _curRound = value; } }

    private SelectType _selectType;

    private void Awake()
    {
        this._cellPrefab = Resources.Load<GameObject>("Prefabs/CellPrefab");
        this._gridList = new GameObject[12][];
        this._gridParent = new GameObject("GridParent");

        this._playerPrefab = Resources.Load<GameObject>("Prefabs/PlayerPrefab");
        this._ourPlayerParent = new GameObject("OurPlayerParent");
        this._theirPlayerParent = new GameObject("TheirPlayerParent");

        this._petPrefab = Resources.Load<GameObject>("Prefabs/PetPrefab");
    }

    private void Update()
    {
        this.UpdateInput();
    }

    public void StartGame()
    {
        this.CreateGrid();
        this.CreatePlayer();
        MainPanel.Instance.CreateWarUI();
        this._selectType = SelectType.Single;
    }

    private void CreateGrid()
    {
        for (int i = 0; i < ConfigData.GridSize; i++)
        {
            for (int j = 0; j < ConfigData.GridSize; j++)
            {
                GameObject obj = GameObject.Instantiate(this._cellPrefab);
                obj.transform.parent = this._gridParent.transform;
                obj.transform.localPosition = DataUtils.GetPosByRank(i, j);
                obj.tag = "Ground";
            }
        }
    }

    private void CreatePlayer()
    {
        this._theirPlayerDic.Clear();
        this._ourPlayerDic.Clear();

        if (this.OurStance != null)
        {
            Vector2Int[] ranks = this.OurStance.Ranks;
            for (int i = 0; i < ranks.Length; i++)
            {
                GameObject obj = GameObject.Instantiate(this._playerPrefab);
                obj.transform.parent = this._ourPlayerParent.transform;
                obj.transform.localPosition = DataUtils.GetPosByRank(ranks[i].x, ranks[i].y);
                obj.tag = "Player";
                Player player = obj.AddComponent<Player>();
                player.FullData(i + 1, Camp.Our);
                player.MaxHp = 6000;
                this._ourPlayerDic.Add(player.Index, player);
            }

            for (int i = 0; i < ConfigData.PetRanks.Length; i++)
            {
                GameObject obj = GameObject.Instantiate(this._petPrefab);
                obj.transform.parent = this._ourPlayerParent.transform;
                obj.transform.localPosition = DataUtils.GetPosByRank(ConfigData.PetRanks[i].x, ConfigData.PetRanks[i].y);
                obj.tag = "Pet";
                this._ourPlayerDic[i + 1].SetPet(obj);
            }
        }

        if (this.TheirStance != null)
        {
            Vector2Int[] ranks = this.TheirStance.Ranks;
            for (int i = 0; i < ranks.Length; i++)
            {
                GameObject obj = GameObject.Instantiate(this._playerPrefab);
                obj.transform.parent = this._theirPlayerParent.transform;
                Vector2Int theirRank = DataUtils.OurRankToTheirRank(ranks[i].x, ranks[i].y);
                obj.transform.localPosition = DataUtils.GetPosByRank(theirRank.x, theirRank.y);
                Player player = obj.AddComponent<Player>();
                player.FullData(i + 1, Camp.Their);
                player.MaxHp = 6000;
                this._theirPlayerDic.Add(player.Index, player);
            }

            for (int i = 0; i < ConfigData.PetRanks.Length; i++)
            {
                GameObject obj = GameObject.Instantiate(this._petPrefab);
                obj.transform.parent = this._ourPlayerParent.transform;
                Vector2Int theirRank = DataUtils.OurRankToTheirRank(ConfigData.PetRanks[i].x, ConfigData.PetRanks[i].y);
                obj.transform.localPosition = DataUtils.GetPosByRank(theirRank.x, theirRank.y);
                this._theirPlayerDic[i + 1].SetPet(obj);
            }
        }
    }

    public void OnAddRound()
    {

    }

    //==================Input===================
    private void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            switch (this._selectType)
            {
                case SelectType.Single:
                    this.SelectType = SelectType.Multi;
                    break;
                case SelectType.Multi:
                    this.SelectType = SelectType.Single;
                    break;
                default:
                    break;
            }

        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            this.SelectType = SelectType.Multi;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            this.SelectType = SelectType.Single;
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Camera.main.transform.position = ConfigData.CameraPos;
            Camera.main.transform.rotation = ConfigData.CameraRot;
            foreach (var item in this._theirPlayerDic.Values)
            {
                item.RefreshUIPos();
            }
            foreach (var item in this._ourPlayerDic.Values)
            {
                item.RefreshUIPos();
            }
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            Camera.main.transform.position = ConfigData.CameraTheirPos;
            Camera.main.transform.rotation = ConfigData.CameraTheirRot;
            foreach (var item in this._theirPlayerDic.Values)
            {
                item.RefreshUIPos();
            }
            foreach (var item in this._ourPlayerDic.Values)
            {
                item.RefreshUIPos();
            }
        }
    }

    //------------------event--------------------
    public void OnPlayerMouse0Up(Player player)
    {
        MainPanel.Instance.SelectPlayer(player);
    }

    public void OnPlayerMouse1Up(Player player)
    {
        List<Player> players = new List<Player>();
        players.Add(player);
        MenuPanel.Instance.Open(players);
    }

    //=================属性==================
    public Player GetPlayer(int index, Camp camp = Camp.Their)
    {
        switch (camp)
        {
            case Camp.Their:
                if (WarManager.Instance.TheirPlayerDic.ContainsKey(index))
                {
                    return WarManager.Instance.TheirPlayerDic[index];
                }
                break;
            case Camp.Our:
                if (WarManager.Instance.OurPlayerDic.ContainsKey(index))
                {
                    return WarManager.Instance.OurPlayerDic[index];
                }
                break;
            default:
                break;
        }
        return null;
    }

    public SelectType SelectType
    {
        get
        {
            return this._selectType;
        }

        set
        {
            this._selectType = value;
            MainPanel.Instance.SetSelectType(this._selectType);
        }
    }
}
