using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpInputField : MonoBehaviour
{
    InputField _hpInputField;
    ArrayList _players;
    Vector3 _originPos;
    bool _active;

    private void Awake()
    {
        this._hpInputField = this.GetComponent<InputField>();
        this.gameObject.SetActive(true);
        this._active = false;
    }

    private void Start()
    {
        this._originPos = this.transform.localPosition;
        this.Close();
    }

    private void Update()
    {
        if (this._active)
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
            else if (Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Minus))
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
            if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                this.OnPlayerClick(1);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                this.OnPlayerClick(2);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                this.OnPlayerClick(3);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
            {
                this.OnPlayerClick(4);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))
            {
                this.OnPlayerClick(5);
            }
        }
    }

    private void OnPlayerClick(int index)
    {
        ArrayList players = new ArrayList();
        players.Add(WarManager.Instance.TheirPlayerDic[index]);
        this.Open(players);
    }

    public void Open(ArrayList players)
    {
        this._active = true;
        this.transform.localPosition = this._originPos;
        this._players = players;
        this._hpInputField.text = "";
        this._hpInputField.ActivateInputField();
    }

    private void Close()
    {
        this._active = false;
        this._hpInputField.text = "";
        this.transform.localPosition = Vector3.one * 10000;
    }

    public string GetStrNum(string str)
    {
        string result = System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", "");
        return result;
    }
}
