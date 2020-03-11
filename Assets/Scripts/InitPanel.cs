using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitPanel : MonoBehaviour
{


    Dropdown theirStanceDropdown;
    Dropdown ourStanceDropdown;

    Dropdown levelDropdown;

    StanceBase theirStance = null;
    StanceBase ourStance = null;

    Button startButton;


    private void Awake()
    {
        this.initUi();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void initUi()
    {
        this.theirStanceDropdown = this.transform.Find("theirStance").GetComponent<Dropdown>();
        this.ourStanceDropdown = this.transform.Find("ourStance").GetComponent<Dropdown>();

        this.theirStanceDropdown.onValueChanged.AddListener(OnTheirStanceSelect);
        this.ourStanceDropdown.onValueChanged.AddListener(OnOurStanceSelect);
        this.theirStanceDropdown.options.Clear();
        this.ourStanceDropdown.options.Clear();
        for (int i = 0; i < ConfigData.StanceEnumList.Length; i++)
        {
            Dropdown.OptionData data = new Dropdown.OptionData();
            data.text = ConfigData.StanceEnumList[i].Name;
            this.theirStanceDropdown.options.Insert(i, data);
            this.ourStanceDropdown.options.Insert(i, data);
        }

        this.startButton = this.transform.Find("startButton").GetComponent<Button>();
        this.startButton.onClick.AddListener(OnStartButtonClick);

        this.levelDropdown = this.transform.Find("levelDropdown").GetComponent<Dropdown>();
        this.levelDropdown.onValueChanged.AddListener(OnLevelSelect);
        this.levelDropdown.options.Clear();

        for (int i = 0; i < ConfigData.LevelList.Length; i++)
        {
            Dropdown.OptionData data = new Dropdown.OptionData();
            data.text = ConfigData.LevelList[i].ToString();
            this.levelDropdown.options.Insert(i, data);
        }
    }

    private void OnStartButtonClick()
    {
        Debug.Log("Start");
        this.gameObject.SetActive(false);
        WarManager.Instance.StartGame();
        MainPanel.Instance.StartGame();
    }

    private void OnTheirStanceSelect(int value)
    {
        this.theirStance = ConfigData.StanceEnumList[value];
        WarManager.Instance.TheirStance = ConfigData.StanceEnumList[value];
    }

    private void OnOurStanceSelect(int value)
    {
        this.ourStance = ConfigData.StanceEnumList[value];
        WarManager.Instance.OurStance = ConfigData.StanceEnumList[value];
    }

    private void OnLevelSelect(int value)
    {
        WarManager.Instance.LevelGroup = ConfigData.LevelList[value];
    }
}
