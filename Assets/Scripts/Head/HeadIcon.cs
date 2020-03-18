using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadIcon : MonoBehaviour
{
    private Text _headIndexText;
    private Text _petCountText;
    private int _petCount;
    private int _prepCount;
    private int _headIndex = 0;
    private Text _prepCountText;

    private void Awake()
    {
        this._headIndexText = this.transform.Find("headText").GetComponent<Text>();
        this._petCountText = this.transform.Find("petText").GetComponent<Text>();
        this._prepCountText = this.transform.Find("prepText").GetComponent<Text>();
        this.transform.Find("petAddBtn").GetComponent<Button>().onClick.AddListener(this.OnAddPetBtnClick);
        this.transform.Find("petSubBtn").GetComponent<Button>().onClick.AddListener(this.OnSubPetBtnClick);
        this.transform.Find("prepAddBtn").GetComponent<Button>().onClick.AddListener(this.OnAddPrepBtnClick);
        this.transform.Find("prepSubBtn").GetComponent<Button>().onClick.AddListener(this.OnSubPrepBtnClick);
        this.transform.Find("babyBtn").GetComponent<Button>().onClick.AddListener(this.OnBabyBtnClick);
    }

    public void Init(int index)
    {
        this._headIndex = index;
        this._headIndexText.text = index.ToString();
        this._petCountText.text = ConfigData.MaxPetCount + "/" + ConfigData.MaxPetCount;
        this._prepCountText.text = ConfigData.MaxPrepCount + "/" + ConfigData.MaxPrepCount;
        this._petCount = ConfigData.MaxPetCount;
        this._prepCount = ConfigData.MaxPrepCount;
    }

    private void OnAddPetBtnClick()
    {
        this._petCount++;
        this._petCount = Mathf.Min(this._petCount, ConfigData.MaxPetCount);
        this._petCountText.text = this._petCount + "/" + ConfigData.MaxPetCount;
    }

    private void OnSubPetBtnClick()
    {
        this._petCount--;
        this._petCount = Mathf.Max(this._petCount, 0);
        this._petCountText.text = this._petCount + "/" + ConfigData.MaxPetCount;
    }

    private void OnAddPrepBtnClick()
    {
        this._prepCount++;
        this._prepCount = Mathf.Min(this._prepCount, ConfigData.MaxPrepCount);
        this._prepCountText.text = this._prepCount + "/" + ConfigData.MaxPrepCount;
    }

    private void OnSubPrepBtnClick()
    {
        this._prepCount--;
        this._prepCount = Mathf.Max(this._prepCount, 0);
        this._prepCountText.text = this._prepCount + "/" + ConfigData.MaxPrepCount;
    }

    private void OnBabyBtnClick()
    {
        this.transform.Find("babyBtn").gameObject.SetActive(false);
    }
}
