﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : Singleton<MainPanel>
{
    Canvas _canvas;
    public Canvas Canvas { get { return this._canvas; } }
    Text reinforceValue;
    Transform reinforceArrow;

    Text levelText;

    GameObject playerIndexPrefab;
    GameObject indexTextParent;

    GameObject hpSliderPrefab;
    GameObject hpSliderParent;

    Button addRoundBtn;
    Text roundText;

    private void Awake()
    {
        _instance = this;
        this.playerIndexPrefab = Resources.Load<GameObject>("UIPrefabs/PlayerIndexPrefab");
        this.hpSliderPrefab = Resources.Load<GameObject>("UIPrefabs/HpSliderPrefab");

        this.initUi();
    }

    void initUi()
    {
        this._canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        this.reinforceValue = this.transform.Find("reinforceValue").GetComponent<Text>();
        this.reinforceArrow = this.transform.Find("reinforceArrow");
        this.levelText = this.transform.Find("levelText").GetComponent<Text>();
        this.indexTextParent = new GameObject("IndexTextParent");
        this.indexTextParent.transform.parent = this.transform;
        this.indexTextParent.transform.localPosition = Vector3.zero;
        this.hpSliderParent = new GameObject("HpSliderParent");
        this.hpSliderParent.transform.parent = this.transform;
        this.hpSliderParent.transform.localPosition = Vector3.zero;
        this.addRoundBtn = this.transform.Find("addRoundBtn").GetComponent<Button>();
        this.addRoundBtn.onClick.AddListener(this.OnAddRoundBtnClick);
        this.roundText = this.transform.Find("roundText").GetComponent<Text>();
        this.roundText.text = "第" + WarManager.Instance.CurRound + "回合";
    }

    private void Update()
    {
        //Debug.Log(Input.mousePosition);
    }

    public void StartGame()
    {
        this.gameObject.SetActive(true);
        this.levelText.text = WarManager.Instance.LevelGroup.ToString() + "组";
        this.RefreshGeinForce();
    }

    private void RefreshGeinForce()
    {
        Debug.Log("RefreshGeinForce");
        if (WarManager.Instance.TheirStance != null && WarManager.Instance.OurStance != null)
        {
            float reinforce = (DataUtils.GetReinforce(WarManager.Instance.TheirStance.Type, WarManager.Instance.OurStance.Type) * 100);
            if (reinforce < 0)
            {
                this.reinforceArrow.transform.rotation = Quaternion.Euler(0, 0, -90);
                this.reinforceValue.text = Mathf.Abs(reinforce).ToString() + "%";
            }
            else if (reinforce > 0)
            {
                this.reinforceArrow.transform.rotation = Quaternion.Euler(0, 0, 90);
                this.reinforceValue.text = Mathf.Abs(reinforce).ToString() + "%";
            }
            else
            {
                this.reinforceArrow.gameObject.SetActive(false);
                this.reinforceValue.text = "无克制";
            }
        }
    }

    public void CreateWarUI()
    {
        for (int i = 1; i <= 5; i++)
        {
            this.InitPlayerUI(WarManager.Instance.GetPlayer(i, Camp.Our));
            this.InitPlayerUI(WarManager.Instance.GetPlayer(i, Camp.Their));
        }
    }

    private void InitPlayerUI(Player player)
    {
        this.InstantiateIndexText(player.Index, player.gameObject);
        PlayerHpSlider slider = this.InstantiateHp(player.Index, player.gameObject);
        player.HpSlider = slider;

        this.InstantiateIndexText(player.Index, player.Pet);
        this.InstantiateHp(player.Index, player.Pet);
    }

    private void InstantiateIndexText(int index, GameObject go)
    {
        GameObject obj = GameObject.Instantiate(this.playerIndexPrefab);
        obj.transform.SetParent(this.indexTextParent.transform);
        obj.GetComponent<Text>().text = index.ToString();
        Vector3 pointPos = go.transform.Find("IndexTextPoint").position;
        obj.transform.localPosition = DataUtils.WorldPosToUIPos(pointPos);
    }

    private PlayerHpSlider InstantiateHp(int index, GameObject go)
    {
        GameObject obj = GameObject.Instantiate(this.hpSliderPrefab);
        PlayerHpSlider script = obj.AddComponent<PlayerHpSlider>();
        obj.transform.SetParent(this.hpSliderParent.transform);
        script.Init();
        Vector3 pointPos = go.transform.Find("hpPoint").position;
        obj.transform.localPosition = DataUtils.WorldPosToUIPos(pointPos);
        Slider slider = obj.GetComponent<Slider>();
        return script;
    }

    private void OnAddRoundBtnClick()
    {
        Debug.Log("OnAddRoundBtnClick");
        WarManager.Instance.CurRound++;
        this.roundText.text = "第" + WarManager.Instance.CurRound + "回合";
        foreach (var item in WarManager.Instance.TheirPlayerDic)
        {
            item.Value.AddRound();
        }

        foreach (var item in WarManager.Instance.OurPlayerDic)
        {
            item.Value.AddRound();
        }
    }
}
