using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int Index;
    public Camp Camp;
    public GameObject Pet;
    public PlayerHpSlider HpSlider;
    public int MaxHp;
    public int CurHp;
    private int _totalSubHp = 0;
    private GameObject _collider;

    private void Awake()
    {
        this._collider = this.transform.Find("Collider").gameObject;
    }

    private void Update()
    {
        //if (Input.GetMouseButtonUp(1))
        //{
        //    Debug.Log("mouse "+ this.Index);
        //    WarManager.Instance.OnPlayerMouseUp(this);
        //}
    }

    public void FullData(int index, Camp camp)
    {
        this.Index = index;
        this.Camp = camp;
        EventManager.AddEvent<GameObject>(EventManager.OnMouse1Up, this.OnMouse2Up);
    }

    public void SetPet(GameObject pet)
    {
        this.Pet = pet;
    }

    public void OnMouse2Up(GameObject obj)
    {
        if (this._collider == obj)
        {
            WarManager.Instance.OnPlayerMouse1Up(this);
        }
    }

    public StanceBase Stance
    {
        get
        {
            switch (this.Camp)
            {
                case Camp.Their:
                    return WarManager.Instance.TheirStance;
                case Camp.Our:
                    return WarManager.Instance.OurStance;
                default:
                    return null;
            }
        }
    }

    public void SetDefaultMaxHp(int value)
    {
        this.MaxHp = value;
        this.CurHp = this.MaxHp - this._totalSubHp;
        this.HpSlider.NumberValue = this.CurHp + "/" + this.MaxHp;
    }

    public void AddHp(int value)
    {
        this.CurHp += value;
        this._totalSubHp -= value;
        this.HpSlider.Value = this.CurHp / this.MaxHp * 1.0f;
        this.HpSlider.NumberValue = this.CurHp + "/" + this.MaxHp;
    }

    public void SubHp(int value)
    {
        this.CurHp -= value;
        this._totalSubHp += value;
        this.HpSlider.Value = this.CurHp / this.MaxHp * 1.0f;
        this.HpSlider.NumberValue = this.CurHp + "/" + this.MaxHp;
    }

    public void Die()
    {
        this.MaxHp = this._totalSubHp;
        this._totalSubHp = 0;
    }

    private int _purdueSurplusRound;
    private int _singlePurdueHp = 0;
    public void AddPurdue(int value)
    {
        this._purdueSurplusRound = ConfigData.Purdue[WarManager.Instance.LevelGroup];
        this._singlePurdueHp = value;
        this.AddHp(value);
    }

    public void DeletePurdue(int value)
    {
        this.AddHp(value);
        this._purdueSurplusRound = 0;
        this._singlePurdueHp = 0;
    }

    public void AddRound()
    {
        if (this._purdueSurplusRound > 0 && this._singlePurdueHp > 0)
        {
            this.AddHp(this._singlePurdueHp);
            this._purdueSurplusRound--;
        }
    }
}
