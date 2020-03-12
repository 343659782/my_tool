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

    public Player GetPlayer(int index, Camp camp)
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

    public void StartGame()
    {
        this.CreateGrid();
        this.CreatePlayer();
        MainPanel.Instance.CreateWarUI();
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
                Player player = obj.AddComponent<Player>();
                player.FullData(i + 1, Camp.Our);
                this._ourPlayerDic.Add(player.Index, player);
            }

            for (int i = 0; i < ConfigData.PetRanks.Length; i++)
            {
                GameObject obj = GameObject.Instantiate(this._petPrefab);
                obj.transform.parent = this._ourPlayerParent.transform;
                obj.transform.localPosition = DataUtils.GetPosByRank(ConfigData.PetRanks[i].x, ConfigData.PetRanks[i].y);
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

    //------------------event--------------------
    public void OnPlayerMouse1Up(Player player)
    {
        Player[] players = new Player[1];
        players.SetValue(player, 0);
        MenuPanel.Instance.Open(players);
    }
}
