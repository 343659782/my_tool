using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    private GameObject initPanel;
    private GameObject mainPanel;

    void Awake()
    {
        this.initPanel = GameObject.Find("InitPanel").gameObject;
        this.mainPanel = GameObject.Find("MainPanel").gameObject;

        Camera.main.transform.position = ConfigData.CameraPos;
        Camera.main.transform.rotation = ConfigData.CameraRot;
        InputManager.Instance.Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = false;
        Screen.SetResolution(1024, 768, false);
        this.initPanel.SetActive(true);
        this.mainPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
