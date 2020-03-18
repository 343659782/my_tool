using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpInputField : MonoBehaviour
{
    InputField _hpInputField;
    List<Player> _players;
    Vector3 _originPos;
    bool _opened;

    WarManager _warMg;

    private void Awake()
    {
        this._hpInputField = this.GetComponent<InputField>();
        this.gameObject.SetActive(true);
        this._opened = false;
    }

    private void Start()
    {
        this._players = new List<Player>();
        this._originPos = this.transform.localPosition;
        this.Close();
        this._warMg = WarManager.Instance;
    }

    private void Update()
    {
        if (this._opened)
        {
            if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Plus))
            {
                string value = GetStrNum(this._hpInputField.text);
                for (int i = 0; i < this._players.Count; i++)
                {
                    (this._players[i] as Player).AddHp(int.Parse(value));
                }
                this.Close();
            }
            else if (Input.GetKeyDown(KeyCode.KeypadMinus) ||
                Input.GetKeyDown(KeyCode.KeypadEnter) ||
                Input.GetKeyDown(KeyCode.Return) ||
                Input.GetKeyDown(KeyCode.Minus))
            {
                string value = GetStrNum(this._hpInputField.text);
                for (int i = 0; i < this._players.Count; i++)
                {
                    (this._players[i] as Player).SubHp(int.Parse(value));
                }
                this.Close();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            {
                this.Open(this._players);
            }
            if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                this.OnKeyDown(1);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                this.OnKeyDown(2);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                this.OnKeyDown(3);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
            {
                this.OnKeyDown(4);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))
            {
                this.OnKeyDown(5);
            }
        }
    }

    private void OnKeyDown(int index)
    {
        Player player = this._warMg.GetPlayer(index);
        this.SelectPlayer(player);
    }

    public void SelectPlayer(Player player)
    {
        switch (this._warMg.SelectType)
        {
            case SelectType.Single:
                this._players.Add(player);
                player.Select();
                this.Open(this._players);
                break;
            case SelectType.Multi:
                if (player.Selected)
                {
                    this._players.Remove(player);
                    player.UnSelect();
                }
                else
                {
                    this._players.Add(player);
                    player.Select();
                }
                break;
            default:
                break;
        }
    }

    public void Open(List<Player> players)
    {
        this._opened = true;
        //Vector3 uiPos = DataUtils.WorldPosToUIPos(players[0].transform.Find("IndexTextPoint").position);
        this.transform.localPosition = this._originPos;
        //this.transform.localPosition = uiPos;
        this._players = players;
        this._hpInputField.text = "";
        this._hpInputField.ActivateInputField();
    }

    private void Close()
    {
        this._opened = false;
        this._hpInputField.text = "";
        this.transform.localPosition = Vector3.one * 10000;
        for (int i = 0; i < this._players.Count; i++)
        {
            this._players[i].UnSelect();
        }
        this._players.Clear();
    }

    public string GetStrNum(string str)
    {
        string result = System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", "");
        return result;
    }
}
