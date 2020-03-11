using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpSlider : MonoBehaviour
{
    private Text _percentValue;
    private Text _numberValue;
    private Slider _slider;
    private void Awake()
    {
        this._percentValue = this.transform.Find("percentValue").GetComponent<Text>();
        this._numberValue = this.transform.Find("numberValue").GetComponent<Text>();
        this._slider = this.GetComponent<Slider>();
    }

    public void Init()
    {
        this._percentValue.text = "100%";
        this._numberValue.text = "0/0";
        this._slider.value = 1f;
    }

    public float Value
    {
        get
        {
            return this._slider.value;
        }
        set
        {
            this._slider.value = value;
        }
    }

    public string PercentValue
    {
        get
        {
            return this._percentValue.text;
        }
        set
        {
            this._percentValue.text = value;
        }
    }

    public string NumberValue
    {
        get
        {
            return this._numberValue.text;
        }
        set
        {
            this._numberValue.text = value;
        }
    }
}
