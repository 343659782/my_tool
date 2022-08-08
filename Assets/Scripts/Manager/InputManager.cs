using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : Singleton<InputManager>
{
    Ray ray;
    RaycastHit hit;
    GameObject obj;

    private void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                obj = hit.collider.gameObject;
                EventManager.DispatchEvent<GameObject>(EventManager.OnMouse0Up, obj);
            }
            else
            {
                WarManager.Instance.ResetMouse0Up();
                WarManager.Instance.ResetMouse1Up();
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                obj = hit.collider.gameObject;
                EventManager.DispatchEvent<GameObject>(EventManager.OnMouse1Up, obj);
                if (hit.collider.gameObject.tag != "Player")
                {
                    WarManager.Instance.ResetMouse1Up();
                }
            }
            else
            {
                WarManager.Instance.ResetMouse1Up();
                WarManager.Instance.ResetMouse0Up();
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            switch (WarManager.Instance.SelectType)
            {
                case SelectType.Single:
                    WarManager.Instance.SelectType = SelectType.Multi;
                    break;
                case SelectType.Multi:
                    WarManager.Instance.SelectType = SelectType.Single;
                    break;
                default:
                    break;
            }

        }else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            WarManager.Instance.SelectType = SelectType.Multi;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            WarManager.Instance.SelectType = SelectType.Single;
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Camera.main.transform.position = ConfigData.CameraPos;
            Camera.main.transform.rotation = ConfigData.CameraRot;
            foreach (var item in WarManager.Instance.TheirPlayerDic.Values)
            {
                item.RefreshUIPos();
            }
            foreach (var item in WarManager.Instance.OurPlayerDic.Values)
            {
                item.RefreshUIPos();
            }
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            Camera.main.transform.position = ConfigData.CameraTheirPos;
            Camera.main.transform.rotation = ConfigData.CameraTheirRot;
            foreach (var item in WarManager.Instance.TheirPlayerDic.Values)
            {
                item.RefreshUIPos();
            }
            foreach (var item in WarManager.Instance.OurPlayerDic.Values)
            {
                item.RefreshUIPos();
            }
        }

    }

    private void OnGUI()
    {
        if (Input.anyKeyDown)
        {
            Event e = Event.current;
            if (e != null)
            {
                Debug.Log(e.keyCode.ToString());
            }
        }
    }
}