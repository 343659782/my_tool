using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadIcon : MonoBehaviour
{
    private Text _headIndexText;
    private Text _petCountText;
    private int _petValue;
    private int _headIndex = 0;

    private void Awake()
    {
        this._headIndexText = this.transform.Find("headText").GetComponent<Text>();
        this._petCountText = this.transform.Find("petText").GetComponent<Text>();
        this.transform.Find("petAddBtn").GetComponent<Button>().onClick.AddListener(this.OnAddBtnClick);
        this.transform.Find("petSubBtn").GetComponent<Button>().onClick.AddListener(this.OnSubBtnClick);
        this.transform.Find("babyBtn").GetComponent<Button>().onClick.AddListener(this.OnBabyBtnClick);
    }

    private void Start()
    {
        this._petCountText.text = ConfigData.MaxPetCount + "/" + ConfigData.MaxPetCount;
        this._petValue = ConfigData.MaxPetCount;
    }

    public void Init(int index)
    {
        this._headIndex = index;
        this._headIndexText.text = index.ToString();
    }

    private void OnAddBtnClick()
    {
        this._petValue++;
        this._petValue = Mathf.Min(this._petValue, ConfigData.MaxPetCount);
        this._petCountText.text = this._petValue + "/" + ConfigData.MaxPetCount;
    }

    private void OnSubBtnClick()
    {
        this._petValue--;
        this._petValue = Mathf.Max(this._petValue,0);
        this._petCountText.text = this._petValue + "/" + ConfigData.MaxPetCount;
    }

    private void OnBabyBtnClick()
    {
        this.transform.Find("babyBtn").gameObject.SetActive(false);
    }
}
